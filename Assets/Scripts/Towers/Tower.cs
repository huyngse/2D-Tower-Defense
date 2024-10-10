using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Element
{
    STORM,
    FIRE,
    ICE,
    POISON,
    NONE
}

public abstract class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TowerRange towerRange;

    [Header("Attributes")]
    [SerializeField]
    private float attackCD = 1;

    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private float bulletSpeed = 3;

    [SerializeField]
    private int price = 5;

    [SerializeField]
    private float debuffDuration = 3;

    [SerializeField]
    private float proc = 50;

    [SerializeField]
    private float range = 1;
    public TowerUpgrade[] Upgrades { get; protected set; }

    private Monster target;
    private bool canAttack = true;
    private float attackTimer = 0;
    private int level = 1;
    private GameObject rangeCircle;
    private readonly List<Monster> monsters = new();
    private Animator animator;
    public Element ElementType { get; protected set; }
    public string BulletType
    {
        get
        {
            switch (ElementType)
            {
                case Element.STORM:
                {
                    return "Storm Bullet";
                }
                case Element.FIRE:
                {
                    return "Fire Bullet";
                }
                case Element.ICE:
                {
                    return "Ice Bullet";
                }
                case Element.POISON:
                {
                    return "Poison Bullet";
                }
            }
            return "";
        }
    }
    public int Price
    {
        get => price;
        set => price = value;
    }
    public int SellPrice
    {
        get => (int)Mathf.Floor(price / 2f);
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
    }
    public int Damage
    {
        get => damage;
        protected set => damage = value;
    }
    public Monster Target
    {
        get => target;
    }
    public float DebuffDuration
    {
        get => debuffDuration;
        protected set => debuffDuration = value;
    }
    public float Proc
    {
        get => proc;
        protected set => proc = value;
    }
    public float AttackCD
    {
        get => attackCD;
        protected set => attackCD = value;
    }
    public int Level
    {
        get => level;
        protected set => level = value;
    }
    public TowerUpgrade NextUpgrade
    {
        get
        {
            if (Upgrades.Length > level)
            {
                return Upgrades[level - 1];
            }
            return null;
        }
    }

    public float Range
    {
        get => range;
        private set => range = value;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rangeCircle = transform.GetChild(0).gameObject;
        rangeCircle.transform.localScale = 5.6f * range * Vector3.one;
    }

    void Update()
    {
        if (target != null)
        {
            if (!target.IsActive || !target.IsAlive)
            {
                monsters.Remove(target);
                return;
            }
            Attack();
        }
        else
        {
            if (monsters.Count > 0)
            {
                target = monsters.First();
            }
        }
    }

    public void Select()
    {
        towerRange.ToggleVisible();
        GameManager.Instance.UpdateUpgradeTooltip();
    }

    public void EnemyEnter(Monster monster)
    {
        monsters.Add(monster);
    }

    public void EnemyExit(Monster monster)
    {
        monsters.Remove(monster);
        target = null;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackCD)
            {
                canAttack = true;
                attackTimer = 0;
            }
            return;
        }
        if (transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        SoundManager.Instance.PlayEffect("shoot-2");
        canAttack = false;
        animator.SetTrigger("Attack");
        Bullet bullet = GameManager.Instance.Pool.GetObject(BulletType).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.SetTower(this);
    }

    public abstract Debuff GetDebuff(Monster target);

    public virtual string GetStats()
    {
        string stats = "";
        stats += "<size=16>";
        if (NextUpgrade != null)
        {
            stats += $"Level: {Level}\n";
            stats += $"Damage: {Damage} (+{NextUpgrade.Damage})\n";
            stats += $"Attack CD: {AttackCD.ToString("F1")}s ({NextUpgrade.AttackCD}s)\n";
            stats += $"Proc: {Proc}% (+{NextUpgrade.Proc}%)\n";
        }
        else
        {
            stats += $"Level: {Level}\n";
            stats += $"Damage: {Damage}\n";
            stats += $"Attack CD: {AttackCD.ToString("F1")}s\n";
            stats += $"Proc: {Proc}%\n";
        }
        stats += "</size>";
        return stats;
    }

    public virtual void Upgrade()
    {
        if (Level == Upgrades.Length)
            return;
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Damage += NextUpgrade.Damage;
        AttackCD += NextUpgrade.AttackCD;
        Proc += NextUpgrade.Proc;
        Price += NextUpgrade.Price;
        Level++;
    }
}

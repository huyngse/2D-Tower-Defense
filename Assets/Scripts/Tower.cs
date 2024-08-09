using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TowerRange towerRange;

    [SerializeField]
    private string bulletType;

    [Header("Attributes")]
    [SerializeField]
    private float attackCD = 1;

    [SerializeField]
    private float bulletSpeed = 3;
    private Monster target;
    private bool canAttack = true;
    private float attackTimer = 0;
    private readonly List<Monster> monsters = new();
    private Animator animator;
    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null && target.IsActive)
        {
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
        canAttack = false;
        animator.SetTrigger("Attack");
        Bullet bullet = GameManager.Instance.Pool.GetObject(bulletType).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.SetSpeed(bulletSpeed);
        bullet.SetTarget(target);
    }
}

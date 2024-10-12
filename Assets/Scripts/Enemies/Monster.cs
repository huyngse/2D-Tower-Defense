using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private string monsterName = "Monster";
    [SerializeField]
    private int baseHP = 5;

    [SerializeField]
    protected float speed = 1;

    [SerializeField]
    protected Stat health;

    [SerializeField]
    private Element elementType;

    [SerializeField]
    private int invulnerability = 2;
    protected Stack<Node> path;
    private List<Debuff> debuffs = new();
    protected Vector3 destination;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public Point GridPosition { get; protected set; }
    public bool IsActive { get; private set; }
    public bool IsAlive
    {
        get { return health.CurrentValue > 0; }
    }

    public Element ElementType
    {
        get => elementType;
    }
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public float BaseSpeed
    {
        get => baseSpeed;
        protected set => baseSpeed = value;
    }
    public List<Debuff> Debuffs
    {
        get => debuffs;
        private set => debuffs = value;
    }
    public string MonsterName { get => monsterName; set => monsterName = value; }

    private float baseSpeed;

    private bool isReachedPortal = false;

    void Awake()
    {
        IsActive = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health.Initialize();
    }

    void Start()
    {
        destination = transform.position;
        BaseSpeed = speed;
    }

    void Update()
    {
        HandleDebuffs();
        Move();
    }

    protected virtual void Move()
    {
        if (path == null || !IsActive)
        {
            return;
        }
        animator.SetBool("isMoving", true);
        transform.position = Vector2.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );
        if (transform.position == destination)
        {
            if (path.Count > 1)
            {
                GridPosition = path.Pop().GridPosition;
                destination = path.Peek().WorldPosition + Vector2.up * 0.5f;
                animator.SetBool("isMoving", true);
                spriteRenderer.sortingOrder = GridPosition.Y + 1;
                Animate();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public void Spawn(float multiplier)
    {
        // health.MaxValue =
        //     baseHP + GameManager.Instance.Wave * Mathf.Pow(GameManager.Instance.Wave, 0.6f);
        health.MaxValue = baseHP * multiplier;
        health.CurrentValue = health.MaxValue;
        transform.position = LevelManager.Instance.GreenPortal.transform.position + Vector3.down * 0.3f;
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));
        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        transform.localScale = to;
        IsActive = true;
        if (isReachedPortal)
        {
            Release();
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;
            if (path.Count == 0)
            {
                path = null;
                Complain();
                return;
            }
            GridPosition = path.Peek().GridPosition;
            destination = path.Peek().WorldPosition;
            spriteRenderer.sortingOrder = GridPosition.Y + 1;
            Animate();
        }
    }

    protected void Animate()
    {
        if (destination.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (destination.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (destination.y > transform.position.y)
        {
            spriteRenderer.flipX = true;
        }
        else if (destination.y < transform.position.y)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReachedPortal && other.CompareTag("PurplePortal"))
        {
            isReachedPortal = true;
            StartCoroutine(Scale(new Vector3(1f, 1f), new Vector3(0.1f, 0.1f)));
            GameManager.Instance.Lifes--;
            SoundManager.Instance.PlayEffect("end-portal");
        }
    }

    protected virtual void Release()
    {
        path = null;
        isReachedPortal = false;
        IsActive = false;
        speed = baseSpeed;
        destination = transform.position;
        animator.speed = 1;
        debuffs.Clear();
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public virtual void TakeDamage(int damage, Element damageSource)
    {
        if (!IsActive || isReachedPortal)
            return;

        if (damageSource == elementType)
        {
            damage = (int)Mathf.Floor(damage / invulnerability);
            invulnerability++;
        }
        health.CurrentValue -= damage;
        animator.SetTrigger("Hit");
        if (health.CurrentValue == 0)
        {
            IsActive = false;
            animator.SetTrigger("Death");
            SoundManager.Instance.PlayEffect($"explosion");
        }
        else
        {
            SoundManager.Instance.PlayEffect($"hit-{Random.Range(1, 5)}");
        }
    }

    public void Death()
    {
        GameManager.Instance.Currency += 4;
        GameObject particle = GameManager.Instance.Pool.GetObject("Death Particle");
        particle.transform.position = transform.position;
        Release();
    }

    public void AddDebuff(Debuff debuff)
    {
        for (int i = 0; i < debuffs.Count; i++)
        {
            Debuff temp = debuffs[0];
            if (temp.GetType() == debuff.GetType())
            {
                debuffs.Remove(temp);
                break;
            }
        }
        debuffs.Add(debuff);
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffs.Remove(debuff);
    }

    public void SetAnimationSpeed(float n)
    {
        animator.speed = n;
    }

    public void SetColor(Color32 color)
    {
        spriteRenderer.color = color;
    }

    private void HandleDebuffs()
    {
        bool isPoisoned = false;
        bool isSlowed = false;
        foreach (Debuff debuff in debuffs.ToList())
        {
            if (debuff.GetType() == typeof(PoisonDebuff))
            {
                isPoisoned = true;
            }
            else if (debuff.GetType() == typeof(IceDebuff))
            {
                isSlowed = true;
            }
            debuff.Update();
        }
        if (isPoisoned)
        {
            if (isPoisoned && isSlowed)
            {
                spriteRenderer.color = new Color32(180, 51, 255, 255);
            }
            else
            {
                spriteRenderer.color = new Color32(84, 255, 104, 255);
            }
        }
        else if (isSlowed)
        {
            spriteRenderer.color = new Color32(84, 244, 255, 255);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void Complain()
    {
        Debug.Log("No path founded");
    }
}

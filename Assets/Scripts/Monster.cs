using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Element elementType;

    [SerializeField]
    private int invulnerability = 2;
    private Stack<Node> path;
    private List<Debuff> debuffs = new();
    private Vector3 destination;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public Point GridPosition { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsAlive
    {
        get { return health.CurrentValue > 0; }
    }
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
    }

    void Update()
    {
        HandleDebuffs();
        Move();
    }

    private void Move()
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
                destination = path.Peek().WorldPosition;
                animator.SetBool("isMoving", true);
                spriteRenderer.sortingOrder = GridPosition.Y;
                Animate();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public void Spawn(int health)
    {
        this.health.MaxValue = health;
        this.health.CurrentValue = health;
        transform.position = LevelManager.Instance.GreenPortal.transform.position;
        // transform.Translate(Vector3.down * 0.9f);
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
            spriteRenderer.sortingOrder = GridPosition.Y;
            Animate();
        }
    }

    private void Animate()
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
        }
    }

    private void Release()
    {
        path = null;
        isReachedPortal = false;
        IsActive = false;
        destination = transform.position;
        debuffs.Clear();
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage(int damage, Element damageSource)
    {
        if (!IsActive)
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
            animator.SetTrigger("Death");
        }
    }

    public void Death()
    {
        GameManager.Instance.Currency += 2;
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

    private void HandleDebuffs() {
        foreach (Debuff debuff in debuffs) {
            debuff.Update();
        }
    }

    private void Complain()
    {
        string dialog = "";
        int randomIndex = Random.Range(0, 10);
        switch (randomIndex)
        {
            case 0:
            {
                dialog =
                    "Ugh, I can't find a way to get there. There's just no clear path and I'm getting so turned around.";
                break;
            }
            case 1:
            {
                dialog =
                    "This is ridiculous, how am I supposed to reach that location when all the routes are blocked off? Seriously, who designed this place?";
                break;
            }
            case 2:
            {
                dialog =
                    "I've been walking in circles for ages trying to find the right way, but it's like this whole area is just a maze with no exits. So frustrating!";
                break;
            }
            case 3:
            {
                dialog =
                    "Hmm, let me see... Nope, that path is a dead end too. Why does it have to be so difficult to get anywhere around here?";
                break;
            }
            case 4:
            {
                dialog =
                    "I swear, the more I try to figure out how to get there, the more confused I become. There's just no clear direction to follow!";
                break;
            }
            case 5:
            {
                dialog =
                    "Ugh, I give up. I've exhausted every possibility and I still can't find a workable route. This is so annoying.";
                break;
            }
            case 6:
            {
                dialog =
                    "I must be missing something obvious, because I can't for the life of me find a way to reach that destination. Where did all the paths go?";
                break;
            }
            case 7:
            {
                dialog =
                    "Unbelievable. I've checked every corner and there's just no clear path forward. Why does this place have to be so complicated?";
                break;
            }
            case 8:
            {
                dialog =
                    "This is ridiculous, I feel like I'm running in circles. How do people navigate this labyrinth of dead ends and blockages?";
                break;
            }
            case 9:
            {
                dialog =
                    "Hmm, let me think this through again... Nope, still nothing. I'm starting to think there isn't even a way to get there from here.";
                break;
            }
        }
        dialog = gameObject.name + ": " + dialog;
        Debug.Log(dialog);
    }
}

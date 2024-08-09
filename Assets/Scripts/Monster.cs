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
    private Stack<Node> path;
    private Vector3 destination;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public Point GridPosition { get; private set; }
    public bool IsActive { get; private set; }
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
        transform.Translate(Vector3.down * 0.9f);
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
                return;
            GridPosition = path.Peek().GridPosition;
            destination = path.Peek().WorldPosition;
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
        isReachedPortal = false;
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage(int damage)
    {
        health.CurrentValue -= damage;
        animator.SetTrigger("Hit");
    }
}

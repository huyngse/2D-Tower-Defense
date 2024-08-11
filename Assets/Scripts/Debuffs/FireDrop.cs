using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDrop : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private float cd = 0.5f;

    [SerializeField]
    private float lifeTime = 5;
    private float lifeTimer;
    private float attackTimer;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            lifeTimer = 0;
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackTimer < cd)
            return;
        if (other.CompareTag("Enemy"))
        {
            Monster target = other.GetComponent<Monster>();
            if (target != null && target.gameObject == other.gameObject)
            {
                target.TakeDamage(damage, Element.FIRE);
                attackTimer = 0;
            }
        }
    }

    public void SetUp(Monster target, float duration)
    {
        lifeTime = duration;
        spriteRenderer.sortingOrder = target.GridPosition.Y;
        transform.position = target.transform.position;
    }
}

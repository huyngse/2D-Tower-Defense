using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifespan = 5;
    private float timer = 0;
    private Animator animator;
    private Tower tower;
    private Monster target;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveToTarget();
        timer = Math.Clamp(Time.deltaTime + timer, 0, 1000);
        if (timer > lifespan)
        {
            Release();
        }
    }

    public void SetTower(Tower tower)
    {
        this.tower = tower;
        target = tower.Target;
    }

    private void MoveToTarget()
    {
        if (target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position,
                tower.BulletSpeed * Time.deltaTime
            );
            RotateTowardTarget();
        }
        else
        {
            Release();
        }
    }

    public void Release()
    {
        timer = 0;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        transform.localScale = Vector3.one;
    }

    private void RotateTowardTarget()
    {
        if (transform.position == target.transform.position)
            return;
        float angle =
            Mathf.Atan2(
                transform.position.y - target.transform.position.y,
                transform.position.x - target.transform.position.x
            ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // transform.rotation = targetRotation;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target != null && target.gameObject == other.gameObject)
            {
                target.TakeDamage(tower.Damage, tower.ElementType);
            }
            transform.localScale = Vector3.one * 2;
            animator.SetTrigger("Hit");
            ApplyDebuff();
        }
    }

    private void ApplyDebuff()
    {
        if (target.ElementType == tower.ElementType) {
            return;
        }
        float randomValue = UnityEngine.Random.Range(0, 100) / 100f;
        if (randomValue < tower.Proc) {
            target.AddDebuff(tower.GetDebuff(target));
        }
    }
}

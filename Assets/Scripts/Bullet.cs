using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Monster target;
    private float speed = 1;
    private float lifespan = 5;
    private float timer = 0;

    void Update()
    {
        MoveToTarget();
        timer = Math.Clamp(Time.deltaTime + timer, 0, 1000);
        if (timer > lifespan)
        {
            Release();
        }
    }

    private void MoveToTarget()
    {
        if (target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position,
                speed * Time.deltaTime
            );
            RotateTowardTarget();
        }
        else
        {
            Release();
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTarget(Monster target)
    {
        this.target = target;
    }

    private void Release()
    {
        timer = 0;
        target = null;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
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
}

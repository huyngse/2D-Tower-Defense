using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Monster
{
    private bool isAngry = false;

    public override void TakeDamage(int damage, Element damageSource)
    {
        if (health.CurrentValue <= health.MaxValue / 2f) {
            isAngry = true;
        }
        if (isAngry) {
            Speed = BaseSpeed * 2;
        }
        animator.SetBool("IsAngry", isAngry);
        base.TakeDamage(damage, damageSource);
    }
    protected override void Release() {
        base.Release();
        isAngry = false;
    }
}

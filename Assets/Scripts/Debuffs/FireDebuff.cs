using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : Debuff
{
    private int tickDamage;

    public FireDebuff(Monster target, float duration, int tickDamage)
        : base(target, duration)
    {
        this.tickDamage = tickDamage;
    }

    public override void Update()
    {
        base.Update();
        GameObject fireDrop = GameManager.Instance.Pool.GetObject("Fire Drop");
        fireDrop.GetComponent<FireDrop>().SetUp(target, duration, tickDamage);
        target.RemoveDebuff(this);
    }
}

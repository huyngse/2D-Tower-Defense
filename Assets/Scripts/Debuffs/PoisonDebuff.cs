using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    private float timeSinceTick;
    private float cd = 1;
    private int tickDamage;

    public PoisonDebuff(Monster target, float duration, int tickDamage)
        : base(target, duration)
    {
        this.tickDamage = tickDamage;
    }

    public override void Update()
    {
        base.Update();
        timeSinceTick += Time.deltaTime;
        if (timeSinceTick > cd)
        {
            timeSinceTick = 0;
            target.TakeDamage(tickDamage, Element.POISON);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    private float timer;
    private float durationTimer;
    private float cd = 1;

    public PoisonDebuff(Monster target, float duration)
        : base(target, duration) { }

    public override void Update()
    {
        timer += Time.deltaTime;
        durationTimer += Time.deltaTime;
        if (timer > cd)
        {
            timer = 0;
            target.TakeDamage(1, Element.POISON);
            base.Update();
        }
        if (durationTimer > duration)
        {
            target.RemoveDebuff(this);
        }
    }
}

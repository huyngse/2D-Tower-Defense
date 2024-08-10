using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    private float timer;
    private float cd = 1;

    public PoisonDebuff(Monster target)
        : base(target) { }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > cd)
        {
            timer = 0;
            target.TakeDamage(1, Element.POISON);
            base.Update();
        }
    }
 
}

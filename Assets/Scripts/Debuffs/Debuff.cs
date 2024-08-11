using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    protected Monster target;
    protected float duration;
    protected float durationTimer;

    public Debuff(Monster target, float duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void Update()
    {
        durationTimer += Time.deltaTime;
        if (durationTimer > duration)
        {
            target.RemoveDebuff(this);
            target.SetColor(Color.white);
        }
    }
}

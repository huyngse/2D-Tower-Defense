using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    protected Monster target;
    protected float duration;

    public Debuff(Monster target, float duration = 3)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void Update() { }
}

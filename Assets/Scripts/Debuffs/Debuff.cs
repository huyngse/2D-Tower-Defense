using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    protected Monster target;

    public Debuff(Monster target)
    {
        this.target = target;
    }

    public virtual void Update() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    void Start()
    {
        ElementType = Element.FIRE;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new FireDebuff(target);
    }
}

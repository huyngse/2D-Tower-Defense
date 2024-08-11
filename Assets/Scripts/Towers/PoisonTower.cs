using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    void Start()
    {
        ElementType = Element.POISON;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new PoisonDebuff(target, DebuffDuration);
    }
}

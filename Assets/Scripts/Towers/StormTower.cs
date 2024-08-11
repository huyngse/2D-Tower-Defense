using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    void Start()
    {
        ElementType = Element.STORM;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new StormDebuff(target, DebuffDuration);
    }
}

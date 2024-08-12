using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    void Start()
    {
        ElementType = Element.STORM;
        Upgrades = new TowerUpgrade[]
        {
            new(20, 1, 5, -0.2f),
            new(23, 1, 5, -0.2f),
            new(28, 1, 5, -0.2f),
            new(31, 1, 5, -0.2f),
            new(36, 1, 5, -0.2f),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new StormDebuff(target, DebuffDuration);
    }
}

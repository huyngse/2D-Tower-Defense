using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    [SerializeField]
    private float slowingFactor = 1.5f;

    public float SlowingFactor
    {
        get => slowingFactor;
    }

    void Start()
    {
        ElementType = Element.ICE;
        Upgrades = new TowerUpgrade[]
        {
            new(20, 1, 5, -0.2f, 1, 0.2f),
            new(23, 0, 0, -0.2f, 0, 0.2f),
            new(28, 1, 5, -0.2f, 1, 0.2f),
            new(31, 1, 0, -0.2f, 1, 0.2f),
            new(36, 1, 5, -0.2f, 1, 0.2f),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new IceDebuff(target, DebuffDuration, slowingFactor);
    }
}

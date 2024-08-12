using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    [SerializeField]
    private float slowingFactor = 1.5f;

    public float SlowingFactor { get => slowingFactor; }

    void Start()
    {
        ElementType = Element.ICE;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new IceDebuff(target, DebuffDuration, slowingFactor);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    void Start()
    {
        ElementType = Element.ICE;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new IceDebuff(target);
    }
}

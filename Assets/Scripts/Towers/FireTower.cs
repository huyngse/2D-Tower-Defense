using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    [SerializeField]
    private int tickDamage = 1;
    public int TickDamage
    {
        get => tickDamage;
        private set => tickDamage = value;
    }

    void Start()
    {
        ElementType = Element.FIRE;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new FireDebuff(target, DebuffDuration, tickDamage);
    }
}

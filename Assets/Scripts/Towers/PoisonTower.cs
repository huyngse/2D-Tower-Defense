using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
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
        ElementType = Element.POISON;
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new PoisonDebuff(target, DebuffDuration, tickDamage);
    }
}

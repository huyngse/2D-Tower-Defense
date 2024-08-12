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
        Upgrades = new TowerUpgrade[]
        {
            new(12, 1, 5, -0.1f, 0, 0),
            new(18, 0, 5, -0.1f, 0, 1),
            new(20, 1, 5, -0.1f, 0, 1),
            new(23, 1, 5, -0.1f, 1, 1),
            new(27, 1, 0, -0.1f, 1, 1),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new PoisonDebuff(target, DebuffDuration, tickDamage);
    }
}

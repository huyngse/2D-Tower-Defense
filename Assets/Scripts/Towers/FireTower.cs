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
        Upgrades = new TowerUpgrade[]
        {
            new(18, 1, 5, -0.2f, 1, 0),
            new(22, 1, 5, -0.2f, 1, 0),
            new(26, 1, 5, -0.2f, 1, 1),
            new(30, 1, 5, -0.2f, 1, 0),
            new(35, 0, 5, -0.2f, 1, 1),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new FireDebuff(target, DebuffDuration, tickDamage);
    }
}

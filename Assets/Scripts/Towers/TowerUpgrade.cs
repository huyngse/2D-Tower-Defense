using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{
    public int Price { get; private set; }
    public int Damage { get; private set; }
    public float DebuffDuration { get; private set; }
    public float Proc { get; private set; }
    public float SlowingFactor { get; private set; }
    public float AttackCD { get; private set; }
    public int TickDamage { get; private set; }

    public TowerUpgrade(
        int price,
        int damage,
        float proc,
        float attackCD
    )
    {
        Price = price;
        Damage = damage;
        Proc = proc;
        AttackCD = attackCD;
    }

    public TowerUpgrade(
        int price,
        int damage,
        float proc,
        float attackCD,
        int debuffDuration,
        float slowingFactor
    )
    {
        Price = price;
        Damage = damage;
        DebuffDuration = debuffDuration;
        Proc = proc;
        AttackCD = attackCD;
        SlowingFactor = slowingFactor;
    }

    public TowerUpgrade(
        int price,
        int damage,
        float proc,
        float attackCD,
        int debuffDuration,
        int tickDamage
    )
    {
        Price = price;
        Damage = damage;
        DebuffDuration = debuffDuration;
        TickDamage = tickDamage;
        Proc = proc;
        AttackCD = attackCD;
    }
}

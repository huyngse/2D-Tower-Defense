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
            new(36, 1, 5, -0.1f, 1, 1),
            new(54, 3, 5, -0.1f, 0, 3),
            new(60, 5, 5, -0.1f, 0, 7),
            new(69, 9, 5, -0.1f, 1, 10),
            new(81, 13, 0, -0.1f, 1, 12),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new PoisonDebuff(target, DebuffDuration, tickDamage);
    }

    public override string GetStats()
    {
        string stats = "<size=24><color=#288248><b>Poison Tower</b></color></size>\n";
        stats += base.GetStats();
        stats += "<size=16>";
        if (NextUpgrade != null)
        {
            stats += $"Poison duration: {DebuffDuration}s (+{NextUpgrade.DebuffDuration})\n";
            stats += $"Poison damage: {TickDamage} (+{NextUpgrade.TickDamage})\n";
        }
        else
        {
            stats += $"Poison duration: {DebuffDuration}s\n";
            stats += $"Poison damage: {TickDamage}\n";
        }
        stats += "<i>Can to <color=#288248>poison</color> enemy</i>.\n";
        stats += "</size>";
        return stats;
    }

    public override void Upgrade()
    {
        if (Level == Upgrades.Length)
            return;
        DebuffDuration += NextUpgrade.DebuffDuration;
        TickDamage += NextUpgrade.TickDamage;
        base.Upgrade();
    }
}

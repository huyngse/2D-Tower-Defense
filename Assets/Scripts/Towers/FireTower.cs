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
            new(45, 1, 10, -0.2f, 1, 1),
            new(66, 2, 10, -0.2f, 1, 2),
            new(78, 4, 10, -0.2f, 1, 4),
            new(90, 8, 10, -0.2f, 1, 6),
            new(105, 12, 10, -0.2f, 1, 8),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new FireDebuff(target, DebuffDuration, tickDamage);
    }

    public override string GetStats()
    {
        string stats = "<size=24><color=#822828><b>Fire Tower</b></color></size>\n";
        stats += base.GetStats();
        stats += "<size=16>";
        if (NextUpgrade != null)
        {
            stats += $"Fire duration: {DebuffDuration}s (+{NextUpgrade.DebuffDuration})\n";
            stats += $"Fire damage: {TickDamage} (+{NextUpgrade.TickDamage})\n";
        }
        else
        {
            stats += $"Fire duration: {DebuffDuration}s\n";
            stats += $"Fire damage: {TickDamage}\n";
        }
        stats += "<i>Can leave <color=#822828>small fire</color> behind enemy</i>.\n";
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

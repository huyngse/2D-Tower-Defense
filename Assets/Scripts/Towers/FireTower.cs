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
            new(15, 1, 5, 0.2f, 1, 0),
            new(22, 1, 5, 0.2f, 1, 0),
            new(26, 1, 5, 0.2f, 1, 1),
            new(30, 1, 5, 0.2f, 1, 0),
            new(35, 0, 5, 0.2f, 1, 1),
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
}

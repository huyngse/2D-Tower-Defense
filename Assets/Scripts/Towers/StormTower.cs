using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    void Start()
    {
        ElementType = Element.STORM;
        Upgrades = new TowerUpgrade[]
        {
            new(54, 2, 5, -0.2f),
            new(69, 5, 5, -0.2f),
            new(84, 9, 5, -0.2f),
            new(93, 13, 5, -0.2f),
            new(108, 20, 5, -0.2f),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new StormDebuff(target, DebuffDuration);
    }

    public override string GetStats()
    {
        string stats = "<size=24><color=#a66d3c><b>Storm Tower</b></color></size>\n";
        stats += base.GetStats();
        stats += "<size=16>";
        if (NextUpgrade != null)
        {
            stats += $"Stun duration: {DebuffDuration}s (+{NextUpgrade.DebuffDuration})\n";
        }
        else
        {
            stats += $"Stun duration: {DebuffDuration}s\n";
        }
        stats += "<i>Can to <color=#a66d3c>poison</color> enemy</i>.\n";
        stats += "</size>";
        return stats;
    }
}

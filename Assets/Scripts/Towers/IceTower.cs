using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    [SerializeField]
    private float slowingFactor = 0.2f;

    public float SlowingFactor
    {
        get => slowingFactor;
        private set => slowingFactor = value;
    }

    void Start()
    {
        ElementType = Element.ICE;
        Upgrades = new TowerUpgrade[]
        {
            new(20, 1, 5, -0.2f, 1, 0.05f),
            new(23, 3, 0, -0.2f, 0, 0.1f),
            new(28, 6, 5, -0.2f, 1, 0.1f),
            new(31, 10, 0, -0.2f, 1, 0.1f),
            new(36, 14, 5, -0.2f, 1, 0.15f),
        };
    }

    public override Debuff GetDebuff(Monster target)
    {
        return new IceDebuff(target, DebuffDuration, slowingFactor);
    }

    public override string GetStats()
    {
        string stats = "<size=24><color=#283882><b>Ice Tower</b></color></size>\n";
        stats += base.GetStats();
        stats += "<size=16>";
        if (NextUpgrade != null)
        {
            stats += $"Slow duration: {DebuffDuration}s (+{NextUpgrade.DebuffDuration})\n";
            stats += $"Slowness: {SlowingFactor * 100}% (+{NextUpgrade.SlowingFactor * 100}%)\n";
        }
        else
        {
            stats += $"Slow duration: {DebuffDuration}s\n";
            stats += $"Slowness: {SlowingFactor * 100}%\n";
        }
        stats += "<i>Has a chance to <color=#283882>slow down</color> enemy</i>.\n";
        stats += "</size>";
        return stats;
    }

    public override void Upgrade()
    {
        if (Level == Upgrades.Length)
            return;
        DebuffDuration += NextUpgrade.DebuffDuration;
        SlowingFactor += NextUpgrade.SlowingFactor;
        base.Upgrade();
    }
}

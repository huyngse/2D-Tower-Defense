using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [Header("References")]
    [SerializeField]
    private BarScript bar;
    private float maxValue;
    private float currentValue;

    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, 0, maxValue);
            bar.Value = currentValue;
        }
    }

    public float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            bar.MaxValue = maxValue;
        }
    }

    public void Initialize()
    {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}

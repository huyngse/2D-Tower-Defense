using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [Header("Attributes")]
    [SerializeField]
    private readonly BarScript bar;

    [SerializeField]
    private float maxValue;

    [SerializeField]
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

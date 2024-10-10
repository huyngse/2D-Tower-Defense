
using System;
using UnityEngine;

public class WaveEnemy : ICloneable
{
  public string Name { get; set; }
  public float HealthMultiplier { get; set; } = 1;
  public Color Color { get; set; } = Color.red;

    public object Clone()
    {
         return this.MemberwiseClone();
    }
}

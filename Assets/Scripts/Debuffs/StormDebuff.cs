using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDebuff : Debuff
{
    public StormDebuff(Monster target, float duration)
        : base(target, duration) { }

    public override void Update()
    {
        base.Update();
        if (durationTimer < duration)
        {
            target.Speed = 0;
            target.SetAnimationSpeed(0);
        }
        else
        {
            target.Speed = target.BaseSpeed;
            target.SetAnimationSpeed(1);
        }
    }
}

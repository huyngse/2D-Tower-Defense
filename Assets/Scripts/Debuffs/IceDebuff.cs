using UnityEngine;

public class IceDebuff : Debuff
{
    private float slowingFactor;

    public IceDebuff(Monster target, float duration, float slowingFactor)
        : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
    }

    public override void Update()
    {
        base.Update();
        if (durationTimer < duration)
        {
            if (target.Debuffs.Exists(x => x.GetType() == typeof(StormDebuff)))
                return;
            target.Speed = target.BaseSpeed * (1 - slowingFactor);
            target.SetAnimationSpeed(0.5f);
        }
        else
        {
            target.Speed = target.BaseSpeed;
            target.SetAnimationSpeed(1);
        }
    }
}

using UnityEngine;

public class IceDebuff : Debuff
{
    public IceDebuff(Monster target, float duration)
        : base(target, duration) { }

    public override void Update()
    {
        base.Update();
        if (durationTimer < duration)
        {
            if (target.Debuffs.Exists(x => x.GetType() == typeof(StormDebuff)))
                return;
            target.Speed = target.BaseSpeed / 2;
            target.SetAnimationSpeed(0.5f);
        }
        else
        {
            target.Speed = target.BaseSpeed;
            target.SetAnimationSpeed(1);
        }
    }
}

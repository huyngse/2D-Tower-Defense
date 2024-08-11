using UnityEngine;

public class IceDebuff : Debuff
{
    public IceDebuff(Monster target, float duration)
        : base(target, duration) { }

    public override void Update()
    {
        if (durationTimer < duration)
        {
            target.Speed = target.BaseSpeed / 2;
        }
        else
        {
            target.Speed = target.BaseSpeed;
        }
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : Debuff
{
    public FireDebuff(Monster target, float duration)
        : base(target, duration) { }

    public override void Update()
    {
        base.Update();
        GameObject fireDrop = GameManager.Instance.Pool.GetObject("Fire Drop");
        fireDrop.GetComponent<FireDrop>().SetUp(target, duration);
        target.RemoveDebuff(this);
    }
}

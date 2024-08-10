using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float lifeTime = 5;
    private float timer = 0;

    void Start() { 
        float angle = UnityEngine.Random.Range(0, 360);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        timer = Math.Clamp(Time.deltaTime + timer, 0, 1000);
        if (timer > lifeTime)
        {
            Release();
        }
    }
    public void Release()
    {
        timer = 0;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
    }
}

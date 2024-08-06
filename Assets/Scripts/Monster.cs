using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public void Spawn()
    {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;
        transform.Translate(Vector3.down * 0.9f);
        transform.localScale = new Vector3(-1, 1, 1);
    }
}

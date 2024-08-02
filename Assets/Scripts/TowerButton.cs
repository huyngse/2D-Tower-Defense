using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;

    public GameObject TowerPrefab
    {
        get => towerPrefab;
    }
    public Sprite Sprite { get => sprite; }
}

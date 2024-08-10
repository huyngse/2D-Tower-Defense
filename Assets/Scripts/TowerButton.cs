using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private TMP_Text priceText;

    [Header("Attributes")]
    [SerializeField]
    private int price = 10;
    public GameObject TowerPrefab
    {
        get => towerPrefab;
    }
    public Sprite Sprite
    {
        get => sprite;
    }
    public int Price { get => price; }

    void Start()
    {
        priceText.text = price + "$";
    }
}

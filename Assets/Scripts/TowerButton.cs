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
    private int price;
    private Tower tower;
    private Button button;
    public GameObject TowerPrefab
    {
        get => towerPrefab;
    }
    public Sprite Sprite
    {
        get => sprite;
    }
    public int Price
    {
        get => price;
        set => price = value;
    }

    void Awake()
    {
        tower = towerPrefab.GetComponent<Tower>();
        button = GetComponent<Button>();
    }

    void Start()
    {
        price = tower.Price;
        priceText.text = price + "$";
    }

    void Update()
    {
        if (GameManager.Instance.Currency < price)
        {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }
}

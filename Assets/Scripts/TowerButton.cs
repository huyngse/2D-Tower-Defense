using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    [SerializeField]
    private Image towerImage;
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
        towerImage.sprite = sprite;
        GameManager.Instance.CurrencyChanged += CheckCurrency;
        CheckCurrency();
    }

    private void CheckCurrency()
    {
        if (GameManager.Instance.Currency < price)
        {
            button.interactable = false;
            priceText.color = Color.red;
        }
        else
        {
            button.interactable = true;
            priceText.color = Color.black;
        }
    }

    public void ShowInfo(string towerType)
    {
        string tooltip = "";
        switch (towerType)
        {
            case "Ice Tower":
            {
                IceTower tower = towerPrefab.GetComponent<IceTower>();
                tooltip += $"<size=24><color=#283882><b>{towerType}</b></color></size>\n";
                tooltip += $"Damage: {tower.Damage}\n";
                tooltip += $"Attack CD: {tower.AttackCD}s\n";
                tooltip += $"Proc: {tower.Proc}%\n";
                tooltip += $"Slow duration: {tower.DebuffDuration}s\n";
                tooltip += $"Slowness: {Math.Round(1 - 1 / tower.SlowingFactor, 2) * 100}%\n";
                tooltip += "<i>Has a chance to <color=#283882>slow down</color> enemy</i>.\n";
                break;
            }
            case "Fire Tower":
            {
                FireTower tower = towerPrefab.GetComponent<FireTower>();
                tooltip += $"<size=24><color=#822828><b>{towerType}</b></color></size>\n";
                tooltip += $"Damage: {tower.Damage}\n";
                tooltip += $"Attack CD: {tower.AttackCD}s\n";
                tooltip += $"Proc: {tower.Proc}%\n";
                tooltip += $"Fire duration: {tower.DebuffDuration}s\n";
                tooltip += $"Fire damage: {tower.TickDamage}\n";
                tooltip += "<i>Can leave <color=#822828>small fire</color> behind enemy</i>.\n";
                break;
            }
            case "Poison Tower":
            {
                PoisonTower tower = towerPrefab.GetComponent<PoisonTower>();
                tooltip += $"<size=24><color=#288248><b>{towerType}</b></color></size>\n";
                tooltip += $"Damage: {tower.Damage}\n";
                tooltip += $"Attack CD: {tower.AttackCD}s\n";
                tooltip += $"Proc: {tower.Proc}%\n";
                tooltip += $"Poison duration: {tower.DebuffDuration}s\n";
                tooltip += $"Poison damage: {tower.TickDamage}\n";
                tooltip += "<i>Likely to <color=#288248>poison</color> enemy</i>.\n";
                break;
            }
            case "Storm Tower":
            {
                StormTower tower = towerPrefab.GetComponent<StormTower>();
                tooltip += $"<size=24><color=#a66d3c><b>{towerType}</b></color></size>\n";
                tooltip += $"Damage: {tower.Damage}\n";
                tooltip += $"Attack CD: {tower.AttackCD}s\n";
                tooltip += $"Proc: {tower.Proc}%\n";
                tooltip += $"Stun duration: {tower.DebuffDuration}s\n";
                tooltip += "<i>May <color=#a66d3c>stun</color> enemy for short duration</i>.\n";
                break;
            }
        }
        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField]
    private TMP_Text currencyText;
    public TowerButton ClickedButton { get; private set; }
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value.ToString() + "<color=green>$</color>";
        }
    }

    private int currency;

    void Start()
    {
        Currency = 10000;
    }

    void Update()
    {
        HandleCancel();
    }

    public void PickTower(TowerButton towerButton)
    {
        ClickedButton = towerButton;
        Hover.Instance.Activate(ClickedButton.Sprite);
    }

    public void BuyTower()
    {
        ClickedButton = null;
        Hover.Instance.Deativate();
    }

    private void HandleCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickedButton = null;
            Hover.Instance.Deativate();
        }
    }
}

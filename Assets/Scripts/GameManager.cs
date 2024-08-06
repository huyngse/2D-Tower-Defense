using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField]
    private TMP_Text currencyText;

    [Header("Attribute")]
    [SerializeField]
    private int currency = 100;

    public TowerButton ClickedButton { get; private set; }
    public ObjectPool Pool { get; private set; }
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value.ToString() + "<color=green>$</color>";
        }
    }

    void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        Currency = currency;
    }

    void Update()
    {
        HandleCancel();
    }

    public void PickTower(TowerButton towerButton)
    {
        if (Currency >= towerButton.Price)
        {
            ClickedButton = towerButton;
            Hover.Instance.Activate(ClickedButton.Sprite);
        }
    }

    public void BuyTower()
    {
        Currency -= ClickedButton.Price;
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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int monsterIndex = Random.Range(0, 4);
        string type = string.Empty;
        switch (monsterIndex)
        {
            case 0:
            {
                type = "Pig";
                break;
            }
            case 1:
            {
                type = "Rock";
                break;
            }
            case 2:
            {
                type = "Slime";
                break;
            }
            case 3:
            {
                type = "Trunk";
                break;
            }
        }
        Pool.GetObject(type);
        yield return new WaitForSeconds(2.5f);
    }
}

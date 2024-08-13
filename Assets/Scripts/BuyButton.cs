using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TMP_Text priceText;
    private string text;
    private int price;

    private Button button;

    public string Text
    {
        get => text;
        set
        {
            text = value;
            priceText.text = text;
        }
    }
    public int Price
    {
        get => price;
        set => price = value;
    }

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        priceText.text = Text;
        GameManager.Instance.CurrencyChanged += CheckCurrency;
        CheckCurrency();
    }

    private void CheckCurrency()
    {
        if (GameManager.Instance.Currency < Price)
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
}

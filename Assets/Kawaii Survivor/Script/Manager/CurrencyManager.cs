using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [SerializeField] private TextMeshProUGUI[] currencyTexts;
    public int Currency {  get; private set; }

    private void Start()
    {
        Currency = 500;
        Load();
    }

    public static bool IsEnoughMoney(int price) => Instance.Currency >= price;
 
    public void Pay(int payPrice)
    {
        Currency = Mathf.Clamp(Currency, 0, Currency - payPrice);
        UpdateCurrencyDisplay();
    }

    public void Add(int sellPrice)
    {
        Currency += sellPrice;
        UpdateCurrencyDisplay();
    }

    public void Load()
    {
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrencyDisplay()
    {
        for(int i = 0; i < currencyTexts.Length; i++)
        {
            currencyTexts[i].text = Currency.ToString();
        }
    }
}

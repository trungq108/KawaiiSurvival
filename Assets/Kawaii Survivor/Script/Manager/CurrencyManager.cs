using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [SerializeField] private TextMeshProUGUI[] candyCurrencyTexts;
    [SerializeField] private TextMeshProUGUI[] moneyCurrencyTexts;

    public int CandyCurrency { get; private set; }
    public int MoneyCurrency { get; private set; }

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ES3.DeleteFile();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddMoney(300);
        }
    }

    private void OnEnable()
    {
        GameEvent.CandyCollected += CandyCollectedCallBack;
        GameEvent.MoneyCollected += MoneyCollectedCallBack;
    }

    private void OnDisable()
    {
        GameEvent.CandyCollected -= CandyCollectedCallBack;
        GameEvent.MoneyCollected -= MoneyCollectedCallBack;
    }

    private void CandyCollectedCallBack() => AddCandy(1);
    private void MoneyCollectedCallBack() => AddMoney(1);


    public static bool IsEnoughCandy(int price) => Instance.CandyCurrency >= price;
    public static bool IsEnoughMoney(int price) => Instance.MoneyCurrency >= price;
 
    public void Pay(int payPrice)
    {
        CandyCurrency = Mathf.Clamp(CandyCurrency, 0, CandyCurrency - payPrice);
        UpdateCurrencyDisplay();
    }
    public void PayMoney(int payPrice)
    {
        MoneyCurrency = Mathf.Clamp(MoneyCurrency, 0, MoneyCurrency - payPrice);
        ES3.Save<int>("MoneyCurrency", MoneyCurrency);
        UpdateCurrencyDisplay();
    }

    public void AddCandy(int sellPrice)
    {
        CandyCurrency += sellPrice;
        UpdateCurrencyDisplay();
    }
    public void AddMoney(int cashNumer)
    {
        MoneyCurrency += cashNumer;
        ES3.Save<int>("MoneyCurrency", MoneyCurrency);
        UpdateCurrencyDisplay();
    }

    public void Load()
    {
        MoneyCurrency = ES3.Load<int>("MoneyCurrency", 500);
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrencyDisplay()
    {
        for(int i = 0; i < candyCurrencyTexts.Length; i++)
        {
            candyCurrencyTexts[i].text = CandyCurrency.ToString();
        }
        for(int i = 0;i < moneyCurrencyTexts.Length;i++)
        {
            moneyCurrencyTexts[i].text = MoneyCurrency.ToString();
        }
    }
}

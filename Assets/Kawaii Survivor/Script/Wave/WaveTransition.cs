using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WaveTransition : Singleton<WaveTransition>, IGameStateListener
{
    [SerializeField] UpgradeButton[] upgradeButtons;

    private ObjectDataSO[] chestObjectDatas;
    public int ChestCollected {  get; private set; }

    [SerializeField] private ObjectContainer objectContainerPrefab;
    [SerializeField] private Transform objectContainerParent;
    [SerializeField] private GameObject upgradeButtonsParent;
    
    private void OnEnable()
    {
        GameEvent.ChestCollected += ChestCollectedCallBack;
    }

    private void OnDisable()
    {
        GameEvent.ChestCollected -= ChestCollectedCallBack;
    }

    public void ChestCollectedCallBack()
    {
        ChestCollected++;
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                TryOpenChests();
                break;
        }
    }

    private void TryOpenChests()
    {
        objectContainerParent.Clear();
        if (ChestCollected > 0)
        {
            ShowChest();
        }
        else
        {
            ConfigureUpgradeButtons();
        }
    }

    private void ShowChest()
    {
        ChestCollected--;
        upgradeButtonsParent.SetActive(false);

        chestObjectDatas = GameAssets.LoadObjectDatas();
        ObjectDataSO randomData = chestObjectDatas[Random.Range(0, chestObjectDatas.Length)];

        ObjectContainer newContainer = Instantiate(objectContainerPrefab, objectContainerParent);
        newContainer.Configue(randomData);

        newContainer.TakeButton.onClick.AddListener(() => AddObjectCallBack(randomData));
        newContainer.TakeButton.onClick.AddListener(() => TryOpenChests());

        newContainer.SellButton.onClick.AddListener(() => SellObjectCallBack(randomData));
        newContainer.SellButton.onClick.AddListener(() => TryOpenChests());
    }

    public void AddObjectCallBack(ObjectDataSO randomData)
    {
        Player player = GameManager.Instance.Player;
        player.AddObject(randomData);
    }

    private void SellObjectCallBack(ObjectDataSO randomData)
    {
        CurrencyManager.Instance.AddCandy(randomData.SellPrice);
    }

    public void ConfigureUpgradeButtons()
    {
        upgradeButtonsParent.SetActive(true);

        for (int i = 0; i < upgradeButtons.Length; i++) 
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat randomStart = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);
            string startName = Enums.FormatStatName(randomStart);
            Sprite icon = GameAssets.LoadStatIcon(randomStart);

            Action action = UpdateStat(randomStart, out string upgradeValueString);
            upgradeButtons[i].Button.onClick.RemoveAllListeners();
            upgradeButtons[i].Button.onClick.AddListener(() => action?.Invoke());
            upgradeButtons[i].Button.onClick.AddListener(() => CheckPlayerUpgrade());

            upgradeButtons[i].Configure(icon, startName, upgradeValueString);
        }
    }

    public void CheckPlayerUpgrade()
    {
        GameManager.Instance.WaveCompleteCallback();
    }

    public Action UpdateStat(Stat stat, out string upgradeValueString)
    {
        float upgradeValueIndex = Random.Range(0, 10);
        upgradeValueString = "+ " + upgradeValueIndex.ToString() + "%";

        switch (stat)
        {
            case Stat.Armor:
                upgradeValueIndex = Random.Range(0, 10f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.AttackSpeed:
                upgradeValueIndex = Random.Range(10f, 25f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.Attack:
                upgradeValueIndex = Random.Range(10f, 25f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.CriticalChance:
                upgradeValueIndex = Random.Range(10f, 25f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.CriticalPercent:
                upgradeValueIndex = Random.Range(10f, 25);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.Dodge:
                upgradeValueIndex = Random.Range(0, 10f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.HealthRegen:
                upgradeValueIndex = Random.Range(0, 3f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1");
                break;
            case Stat.LifeSteal:
                upgradeValueIndex = Random.Range(0, 5f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
            case Stat.Luck:
                upgradeValueIndex = Random.Range(0, 1f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;

            case Stat.MaxHealth:
                upgradeValueIndex = Random.Range(0, 30);
                upgradeValueString = "+ " + upgradeValueIndex.ToString();
                break;

            case Stat.MoveSpeed:
                upgradeValueIndex = Random.Range(0, 1f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1");
                break;

            case Stat.Range:
                upgradeValueIndex = Random.Range(10f, 25f);
                upgradeValueString = "+ " + upgradeValueIndex.ToString("F1") + "%";
                break;
        }
        return () => PlayerStatManager.Instance.AddStatData(stat, upgradeValueIndex);
    }

}

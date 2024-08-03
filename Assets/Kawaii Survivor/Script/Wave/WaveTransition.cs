using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WaveTransition : MonoBehaviour, IGameStateListener
{
    [SerializeField] UpgradeButton[] upgradeButtons;

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeButtons();
                break;
        }
    }
    public void ConfigureUpgradeButtons()
    {
        for (int i = 0; i < upgradeButtons.Length; i++) 
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat randomStart = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);
            string startName = Enums.FormatStatName(randomStart);

            Action action = UpdateStat(randomStart, out string upgradeValueString);
            upgradeButtons[i].Button.onClick.RemoveAllListeners();
            upgradeButtons[i].Button.onClick.AddListener(() => action?.Invoke());
            upgradeButtons[i].Button.onClick.AddListener(() => CheckPlayerUpgrade());

            upgradeButtons[i].Configure(null, startName, upgradeValueString);
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

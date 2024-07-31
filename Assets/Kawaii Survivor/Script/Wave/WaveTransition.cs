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
    private List<TextMeshProUGUI> upgradeTexts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            TextMeshProUGUI upgradeText = upgradeButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            upgradeTexts.Add(upgradeText);
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
        int upgradeValueIndex = Random.Range(0, 10);
        upgradeValueString = "+ " + upgradeValueIndex.ToString() + "%";

        switch (stat)
        {
            case Stat.Armor:
                Debug.Log(upgradeValueString);

                break;
            case Stat.AttackSpeed:
                Debug.Log(upgradeValueString);

                break;
            case Stat.Attack:
                Debug.Log(upgradeValueString);

                break;
            case Stat.CriticalChance:
                Debug.Log(upgradeValueString);

                break;
            case Stat.CriticalPercent:
                Debug.Log(upgradeValueString);

                break;
            case Stat.Dodce:
                Debug.Log(upgradeValueString);

                break;
            case Stat.HealthRegen:
                Debug.Log(upgradeValueString);

                break;
            case Stat.LifeSteal:
                Debug.Log(upgradeValueString);

                break;
            case Stat.Luck:
                Debug.Log(upgradeValueString);

                break;
            case Stat.MaxHealth:
                Debug.Log(upgradeValueString);

                break;
            case Stat.MoveSpeed:
                Debug.Log(upgradeValueString);

                break;
            case Stat.Range:
                Debug.Log(upgradeValueString);

                break;
        }
        return () => Debug.Log("Procesing");
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeButtons();
                break;
        }
    }
}

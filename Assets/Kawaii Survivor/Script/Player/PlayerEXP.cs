using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerEXP : MonoBehaviour
{
    [SerializeField] private int levelEXP;
    private int currentEXP = 0;
    private int currentLevel = 1;

    private int levelsEarnThisLevel;

    [SerializeField] Slider EXPBar;
    [SerializeField] TextMeshProUGUI EXPText;

    private void OnEnable()
    {
        Candy.onCollected += IncreaseEXP;
        EXPBarUpdate();
    }

    private void OnDestroy()
    {
        Candy.onCollected -= IncreaseEXP;
    }


    private void IncreaseEXP()
    {
        currentEXP++;
        EXPBarUpdate();
        if (currentEXP > levelEXP)
        {
            IncreaseLevel();
        }
    }

    private void IncreaseLevel()
    {
        currentLevel++;
        levelsEarnThisLevel++;
        currentEXP = 0;
        levelEXP = (currentLevel + 1) * levelEXP;

        EXPBarUpdate();
    }

    private void EXPBarUpdate()
    {
        EXPBar.value = (float)currentEXP / levelEXP;
        EXPText.text = "LV " + currentLevel;
    }

    public bool HasLevelUp()
    {
        return true;
        if (levelsEarnThisLevel > 0)
        {
            levelsEarnThisLevel--;
            return true;
        }
        else return false;
    }
}

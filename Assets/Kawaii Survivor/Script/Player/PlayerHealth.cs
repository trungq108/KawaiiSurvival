using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IStatDependency
{
    [Header("Setting")]
    private int maxHealth;
    private int currentHealth;
    
    [Header("Element")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    public void TakeDame(int damage)
    {
        int realDamageTaken = Mathf.Min(currentHealth, damage);
        currentHealth -= realDamageTaken;
        HealthBarUpdate();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void HealthBarUpdate()
    {
        healthBar.value = (float) currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }

    private void Death()
    {
        GameManager.Instance.SetGameState(GameState.GAMEOVER);
    }

    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        int upgradeHealth = (int)playerStatManager.GetStatData(Stat.MaxHealth);
        maxHealth = upgradeHealth;
        currentHealth = maxHealth;
        HealthBarUpdate();
    }
}

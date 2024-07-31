using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Setting")]
    private int currentHealth;
    [SerializeField] int maxHealth;
    
    [Header("Element")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    private void Awake()
    {
        currentHealth = maxHealth;
        HealthBarUpdate();
    }

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
}

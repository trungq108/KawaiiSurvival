using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] int maxHealth;
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
    }

    public void HealthBarUpdate()
    {
        healthBar.value = (float) currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        SceneManager.LoadScene(0);
    }
}

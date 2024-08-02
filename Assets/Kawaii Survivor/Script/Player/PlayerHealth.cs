using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IStatDependency
{
    [Header("Setting")]
    private float maxHealth;
    private float currentHealth;
    private float armor;
    private float lifeSteal;
    private float dodge;
    private float healthRegen;

    private float timer;
    
    [Header("Element")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] DamageText damageText;

    private void OnEnable()
    {
        GameEvent.HitEnemy += OnLifeSteal;
    }

    private void OnDisable()
    {
        GameEvent.HitEnemy -= OnLifeSteal;
    }

    private void Update()
    {
        if(currentHealth < maxHealth) HealthRegen();
    }

    private void HealthRegen()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0f;
            currentHealth += healthRegen;
            UpdateHealthBar();
        }
    }

    public void TakeDame(int baseDamageTaken)
    {
        if (Dodge())
        {
            DamageText textDamage = LeanPool.Spawn(damageText, this.transform.position + Vector3.up, Quaternion.identity);
            textDamage.Trigger("Dodge");
            LeanPool.Despawn(textDamage, 0.5f);
            return;
        }

        float realDamageTaken = baseDamageTaken * (1 - armor / 100);
        realDamageTaken = Mathf.Min(currentHealth, realDamageTaken); 
        currentHealth -= realDamageTaken;
        UpdateHealthBar();
        if (currentHealth <= 0) Death();
    }

    private bool Dodge()
    {
        return Random.Range(0f, 100f) < dodge;
    }

    private void Death()
    {
        GameManager.Instance.SetGameState(GameState.GAMEOVER);
    }

    public void OnLifeSteal(Enemy enemy, int lifeStealAmount)
    {
        if (currentHealth >= maxHealth) return;

        float addHealth = lifeStealAmount * lifeSteal / 100; 
        Debug.Log(addHealth);
        currentHealth += addHealth;
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
        healthText.text = (int)currentHealth + "/" + maxHealth;
    }

    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        float upgradeHealth = playerStatManager.GetStatData(Stat.MaxHealth);
        maxHealth = upgradeHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();

        armor = playerStatManager.GetStatData(Stat.Armor);
        lifeSteal = playerStatManager.GetStatData(Stat.LifeSteal);
        dodge = playerStatManager.GetStatData(Stat.Dodge);
        healthRegen = playerStatManager.GetStatData(Stat.HealthRegen);
    }
}

using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IPlayerStatDependency
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
        if(currentHealth < maxHealth)
        {
            HealthRegen();
        }
    }

    private void HealthRegen()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0f;
            currentHealth = Mathf.Min(currentHealth + healthRegen, maxHealth);
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
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth - realDamageTaken);
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
        Destroy(this.gameObject);
    }

    public void OnLifeSteal(Enemy enemy, int damage)
    {
        if (currentHealth >= maxHealth) return;

        float addHealth = damage * lifeSteal / 100;
        currentHealth = Mathf.Min(currentHealth + addHealth, maxHealth);
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
        healthText.text = (int)currentHealth + "/" + maxHealth;
    }

    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        float upgradeHealth = playerStatManager.GetStat(Stat.MaxHealth);
        maxHealth = upgradeHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();

        armor = playerStatManager.GetStat(Stat.Armor);
        lifeSteal = playerStatManager.GetStat(Stat.LifeSteal);
        dodge = playerStatManager.GetStat(Stat.Dodge);
        healthRegen = playerStatManager.GetStat(Stat.HealthRegen);
    }
}

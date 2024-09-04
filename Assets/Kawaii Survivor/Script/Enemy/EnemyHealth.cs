using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Pool;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] float maxHealth;

    [Header("VFX")]
    [SerializeField] GameObject bloodVFXPrefab;
    [SerializeField] DamageText damageText;

    [Header("Boss Element")]
    [SerializeField] bool isBoss;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    public void OnInit()
    {
        currentHealth = maxHealth;
        if (isBoss) UpdateHealthBar();
        healthBar.gameObject.SetActive(false);
    }

    public void OnInitCompleted()
    {
        healthBar.gameObject.SetActive(true);
    }

    public void TakeDame(int damage, bool isCritical)
    {
        float realDamageTaken = Mathf.Min(currentHealth, damage);
        currentHealth -= realDamageTaken;

        DamageText textDamage = LeanPool.Spawn(damageText, this.transform.position, Quaternion.identity);
        textDamage.Trigger(damage, isCritical);
        LeanPool.Despawn(textDamage, 0.5f);

        GameObject bloodVFX = LeanPool.Spawn(bloodVFXPrefab, this.transform.position, Quaternion.identity);
        LeanPool.Despawn(bloodVFX, 2f);

        if (currentHealth <= 0) Death();
        if (isBoss) UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
        healthText.text = (int)currentHealth + "/" + maxHealth;
    }

    private void Death()
    {
        if(!isBoss) DropItemManager.Instance.DropItem(this.transform);
        else DropItemManager.Instance.DropChest(this.transform);
        LeanPool.Despawn(this.gameObject);
    }
}

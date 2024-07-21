using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Pool;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] int maxHealth;

    [Header("VFX")]
    [SerializeField] GameObject bloodVFXPrefab;
    [SerializeField] DamageText damageText;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDame(int damage, bool isCritical)
    {
        int realDamageTaken = Mathf.Min(currentHealth, damage);
        currentHealth -= realDamageTaken;

        DamageText textDamage = LeanPool.Spawn(damageText, this.transform.position, Quaternion.identity);
        textDamage.Trigger(damage, isCritical);
        LeanPool.Despawn(textDamage, 1f);

        GameObject bloodVFX = LeanPool.Spawn(bloodVFXPrefab, this.transform.position, Quaternion.identity);
        LeanPool.Despawn(bloodVFX, 2f);

        if (currentHealth <= 0) Death();
    }

    private void Death()
    {
        DropItemManager.Instance.Drop(this.transform);
        LeanPool.Despawn(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Pool;

public class EnemyHealth : MonoBehaviour
{
    [Header("Setting")]
    private int currentHealth;
    [SerializeField] int maxHealth;


    //[Header("Element")]


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDame(int damage)
    {
        int realDamageTaken = Mathf.Min(currentHealth, damage);
        currentHealth -= realDamageTaken;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        LeanPool.Despawn(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth)), RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerController controller;
    private PlayerDetect detect;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
        detect = GetComponent<PlayerDetect>();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDame(damage);
    }

    public void IncreaseEXP(int amount)
    {
        Debug.Log(amount);
    }
}

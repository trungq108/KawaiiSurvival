using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerController controller;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDame(damage);
    }
}

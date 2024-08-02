using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth)), RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerController controller;
    private PlayerDetect detect;
    private PlayerEXP level;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
        detect = GetComponent<PlayerDetect>();
        level = GetComponent<PlayerEXP>();
    }

    public void TakeDamage(int baseDamageTaken)
    {
        health.TakeDame(baseDamageTaken);
    }

    public bool HasLevelUp()
    {
        return level.HasLevelUp();
    }
}

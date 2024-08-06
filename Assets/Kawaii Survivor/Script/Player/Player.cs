using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth)), RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerController controller;
    private PlayerDetect detect;
    private PlayerEXP level;
    private PlayerWeapon playerWeapon;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
        detect = GetComponent<PlayerDetect>();
        level = GetComponent<PlayerEXP>();
        playerWeapon= GetComponent<PlayerWeapon>();
    }

    public void TakeDamage(int baseDamageTaken)
    {
        health.TakeDame(baseDamageTaken);
    }

    public bool HasLevelUp()
    {
        return level.HasLevelUp();
    }

    internal void AddWeapon(WeaponDataSO data, int weaponLevel)
    {
        playerWeapon.AddWeapon(data, weaponLevel);
    }
}

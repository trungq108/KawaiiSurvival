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
    private PlayerObjects playerObjects;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
        detect = GetComponent<PlayerDetect>();
        level = GetComponent<PlayerEXP>();
        playerWeapon= GetComponent<PlayerWeapon>();
        playerObjects= GetComponent<PlayerObjects>();
    }

    public void TakeDamage(int baseDamageTaken) => health.TakeDame(baseDamageTaken);

    public bool HasLevelUp() => level.HasLevelUp();

    public bool TryAddWeapon(WeaponDataSO data, int weaponLevel) => playerWeapon.TryAddWeapon(data, weaponLevel);
    public void AddObject(ObjectDataSO objectData) => playerObjects.AddObject(objectData);
    public List<Weapon> GetWeapons() => playerWeapon.weaponList;
    public List<ObjectDataSO> GetObjects() => playerObjects.Objects;
    public void RemoveWeapon(Weapon weapon) => playerWeapon.RemoveWeapon(weapon);
    public void RemoveObject(ObjectDataSO objectData) => playerObjects.RemoveObject(objectData);


}

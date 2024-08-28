using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MergeSystem
{
    private static List<Weapon> weaponToMerge = new List<Weapon>();

    public static bool CanMerge(Weapon weapon)
    {
        if (weapon.weaponLevel >= 3) return false;
        weaponToMerge.Clear();
        weaponToMerge.Add(weapon);

        List<Weapon> weaponList = GameManager.Instance.Player.GetWeapons();
        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon checkWeapon = weaponList[i];
            if (checkWeapon == weapon) continue;
            if (checkWeapon.weaponLevel != weapon.weaponLevel) continue;
            if (checkWeapon.Data.WeaponName != weapon.Data.WeaponName) continue;

            weaponToMerge.Add(checkWeapon);
            return true;
        }
        return false;
    }

    public static void Merge()
    {
        if (weaponToMerge.Count < 2)
        {
            Debug.LogError("Some thing wrong with merge list");
            return;
        }

        Weapon resultWeapon = weaponToMerge[0];
        Weapon destroyWeapon = weaponToMerge[1];
        weaponToMerge.Clear();

        GameManager.Instance.Player.RemoveWeapon(destroyWeapon);
        resultWeapon.Upgrade();
        GameEvent.OnWeaponMerge?.Invoke(resultWeapon);
    }
}

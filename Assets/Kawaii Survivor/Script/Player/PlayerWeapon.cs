using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform[] weaponPos;
    public List<Weapon> weaponList {  get; private set; } = new List<Weapon>();

    public bool TryAddWeapon(WeaponDataSO data, int weaponLevel)
    {
        if (GetAvailablePos() == null)
        {
            Debug.Log("OUT OF SLOT WEAPON !!!");
            return false;
        }
        else
        {
            AddWeapon(data, weaponLevel);
            return true;
        }        
    }

    public void AddWeapon(WeaponDataSO data, int weaponLevel)
    {
        Weapon newWeapon = Instantiate(data.Prefab, GetAvailablePos()).GetComponentInChildren<Weapon>();
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.SetInitLevel(weaponLevel);
        weaponList.Add(newWeapon);
    }

    private Transform GetAvailablePos()
    {
        List<Transform> availablePos = new List<Transform>();

        for (int i = 0; i < weaponPos.Length; i++)
        {
            if (weaponPos[i].childCount == 0)
            {
                availablePos.Add(weaponPos[i]);
            }
        }
        return availablePos[Random.Range(0, availablePos.Count)];
    }
}

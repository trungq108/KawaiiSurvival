using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform[] weaponPos;
    private List<Weapon> weaponList = new List<Weapon>();
    
    public void AddWeapon(WeaponDataSO data, int weaponLevel)
    {
        if (GetAvailablePos() == null)
        {
            Debug.Log("We Out Of Weapon Slot !!!");
            return;
        }            
        else
        {
            Weapon newWeapon = Instantiate(data.Prefab, GetAvailablePos()).GetComponentInChildren<Weapon>();
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.SetInitLevel(weaponLevel);
            weaponList.Add(newWeapon);
        }    
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

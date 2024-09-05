using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : Singleton<DropItemManager>
{
    [SerializeField] Candy candyPrefab;
    [SerializeField] Money moneyPrefab;
    [SerializeField] Chest chestPrefab;

    [SerializeField] int candyDropRate;
    [SerializeField] int chestDropRate;


    public void DropItem(Transform pos)
    {
        int rate = Random.Range(0, 10);
        if(rate < candyDropRate)
        {
            LeanPool.Spawn(candyPrefab, pos.position, Quaternion.identity, this.transform);
        }
        else LeanPool.Spawn(moneyPrefab, pos.position, Quaternion.identity, this.transform);

        IsDropChest(pos);
    }

    private void IsDropChest(Transform pos)
    {
        int rate = Random.Range(0, 10);
        if (rate < chestDropRate) 
        {
            DropChest(pos);
        }
    }

    public void DropChest(Transform pos)
    {
        LeanPool.Spawn(chestPrefab, pos.position, Quaternion.identity, this.transform);
    }
}

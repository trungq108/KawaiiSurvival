using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : Singleton<DropItemManager>
{
    [SerializeField] Candy candyPrefab;
    [SerializeField] Money moneyPrefab;
    [SerializeField] Chest chestPrefab;


    public void Drop(Transform pos)
    {
        int rate = Random.Range(0, 10);
        if(rate < 8)
        {
            LeanPool.Spawn(candyPrefab, pos.position, Quaternion.identity, this.transform);
        }
        else LeanPool.Spawn(moneyPrefab, pos.position, Quaternion.identity, this.transform);

        IsDropChest(pos);
    }

    private void IsDropChest(Transform pos)
    {
        int rate = Random.Range(0, 10);
        if (rate < 1) 
        {
            LeanPool.Spawn(chestPrefab, pos.position, Quaternion.identity, this.transform);
        }
    }
}

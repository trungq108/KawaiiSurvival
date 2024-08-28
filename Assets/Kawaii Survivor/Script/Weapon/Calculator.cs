using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator 
{
    public static Dictionary<Stat, float> WeaponStats(WeaponDataSO data, int level)
    {
        float multiple = 1 + (float)level / 3;

        Dictionary<Stat, float> caculatorStats = new Dictionary<Stat, float>();
        foreach (KeyValuePair<Stat,float> kvp in data.BaseStats)
        {

            if(kvp.Key == Stat.Range && data.Prefab.transform.GetChild(0).GetType() != typeof(RangeWeapon))
            {
                caculatorStats.Add(kvp.Key, kvp.Value);
            }
            else caculatorStats.Add(kvp.Key, kvp.Value * multiple);
        }

        return caculatorStats;
    }

    public static int WeaponPrice(WeaponDataSO data, int level)
    {;
        int price = (int)(data.WeaponPrice * Mathf.Pow(1.33f, level));
        return price;
    }
    public static int WeaponRecyclePrice(WeaponDataSO data, int level)
    {

        int recyclePrice = (int)(data.WeaponRecyclePrice * Mathf.Pow(1.33f, level));
        return recyclePrice;
    }
}

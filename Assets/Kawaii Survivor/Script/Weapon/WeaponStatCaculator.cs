using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponStatCaculator 
{
    public static Dictionary<Stat, float> Caculator(WeaponDataSO data, int level)
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
}

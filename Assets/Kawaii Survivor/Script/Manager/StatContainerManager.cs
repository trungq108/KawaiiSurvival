using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatContainerManager : Singleton<StatContainerManager>
{
    [SerializeField] StatContainer containerPrefab;

    public void CreatContainer(WeaponDataSO data, int level, Transform parent)  
    {
        Dictionary<Stat, float> caculate = WeaponStatCaculator.Caculator(data, level);

        foreach (KeyValuePair<Stat, float> kvp in caculate)
        {
            Sprite icon = GameAssets.LoadStatIcon(kvp.Key);
            string name = Enums.FormatStatName(kvp.Key);
            float value = kvp.Value;

            StatContainer newStatContainer = Instantiate(containerPrefab, parent);
            newStatContainer.Confingue(icon, name, value);
        }
    }

}

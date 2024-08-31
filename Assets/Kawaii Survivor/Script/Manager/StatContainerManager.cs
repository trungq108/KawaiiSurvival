using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatContainerManager : Singleton<StatContainerManager>
{
    [SerializeField] StatContainer containerPrefab;

    public void CreatContainers(Dictionary<Stat, float> statData, Transform parent)  
    {
        parent.Clear();
        foreach (KeyValuePair<Stat, float> kvp in statData)
        {
            Sprite icon = GameAssets.LoadStatIcon(kvp.Key);
            string name = Enums.FormatStatName(kvp.Key);
            float value = kvp.Value;

            StatContainer newStatContainer = Instantiate(containerPrefab, parent);
            newStatContainer.Confingue(icon, name, value);
        }
    }
}

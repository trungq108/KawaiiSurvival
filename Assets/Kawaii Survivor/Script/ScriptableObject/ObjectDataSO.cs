using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "Scriptable Object / ObjectDataSO")]

public class ObjectDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int SellPrice { get; private set; }

    [field: SerializeField, Range(0,3)] public int RareRate { get; private set; }

    [SerializeField] StatData[] statDatas;
    public Dictionary<Stat, float> BaseData
    {
        get
        {
            Dictionary<Stat, float> newData = new Dictionary<Stat, float>();
            for (int i = 0; i < statDatas.Length; i++) 
            {
                newData.Add(statDatas[i].stat, statDatas[i].value);
            }
            return newData;
        }
        private set { }
    }
}

[System.Serializable]
public struct StatData
{
    public Stat stat;
    public float value;
}

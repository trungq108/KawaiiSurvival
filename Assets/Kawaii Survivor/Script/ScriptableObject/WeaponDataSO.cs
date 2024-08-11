using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Object /WeaponDataSO", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public Sprite WeaponSprite { get; private set; }
    [field: SerializeField] public int WeaponPrice { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }

    [SerializeField] private float AttackSpeed;
    [SerializeField] private float Attack;
    [SerializeField] private float CriticalChance;
    [SerializeField] private float CriticalPercent;
    [SerializeField] private float Range;

    public Dictionary<Stat, float> BaseStats
    {
        get
        {
            return new Dictionary<Stat, float>()
            {
               {Stat.AttackSpeed, AttackSpeed},
               {Stat.Attack, Attack},
               {Stat.CriticalChance, CriticalChance},
               {Stat.CriticalPercent, CriticalPercent},
               {Stat.Range, Range},
            };
        }
        private set { }
    }

    public float GetStat(Stat stat)
    {
        if(BaseStats.ContainsKey(stat))
        {
            return BaseStats[stat];
        }
        Debug.Log("Weapon Data Don't Have This Stat");
        return 0; 
    }
}

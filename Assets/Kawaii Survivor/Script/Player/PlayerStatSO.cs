using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "ScriptableOject", order = 0)]
public class PlayerStatSO : ScriptableObject
{
    [field :SerializeField] public string PlayerName {  get; private set; }
    [field: SerializeField] public Sprite PlayerSprite { get; private set; }
    [field: SerializeField] public int PlayerPrice { get; private set; }

    [SerializeField] private float Armor;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float Attack;
    [SerializeField] private float CriticalChance;
    [SerializeField] private float CriticalPercent;
    [SerializeField] private float Dodce;
    [SerializeField] private float HealthRegen;
    [SerializeField] private float LifeSteal;
    [SerializeField] private float Luck;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Range;

    public Dictionary<Stat, float> BaseStats 
    {
        get
        {
            return new Dictionary<Stat, float>()
            {
               {Stat.Armor, Armor},
               {Stat.AttackSpeed, AttackSpeed},
               {Stat.Attack, Attack},
               {Stat.CriticalChance, CriticalChance},
               {Stat.CriticalPercent, CriticalPercent},
               {Stat.Dodce, Dodce},
               {Stat.HealthRegen, HealthRegen},
               {Stat.LifeSteal, LifeSteal},
               {Stat.Luck, Luck},
               {Stat.MaxHealth, MaxHealth},             
               {Stat.MoveSpeed, MoveSpeed},
               {Stat.Range, Range},
            };
        }
        private set { }
    }

}

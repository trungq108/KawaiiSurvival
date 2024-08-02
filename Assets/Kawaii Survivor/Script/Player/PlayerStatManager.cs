using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class PlayerStatManager : Singleton<PlayerStatManager>
{
    [SerializeField] PlayerDataSO PlayerStatSO;
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> addStats = new Dictionary<Stat, float>();

    private void Awake()
    {
        playerStats = PlayerStatSO.BaseStats;
        foreach(KeyValuePair<Stat, float> kvp in playerStats)
        {
            addStats.Add(kvp.Key, 0f);
        }
    }

    private void Start()
    {
        UpgradeStats();
    }

    public void AddStatData(Stat stat, float value)
    {
        if (addStats.ContainsKey(stat))
        {
            addStats[stat] += value;
        }
        else Debug.LogError("Stat Data Do Not Have This Stat");

        UpgradeStats();
    }

    public float GetStatData(Stat stat)
    {
        return playerStats[stat] + addStats[stat];
    }

    private void UpgradeStats()
    {
        IEnumerable<IStatDependency> listeners =
                    FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IStatDependency>();

        foreach (var listener in listeners)
        {
            listener.UpdateStat(this);
        }
    }
}

public interface IStatDependency
{
    void UpdateStat(PlayerStatManager playerStatManager);
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class PlayerStatManager : Singleton<PlayerStatManager>, IGameStateListener
{
    [SerializeField] PlayerDataSO PlayerStatSO;
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> addStats = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> addObjectStat = new Dictionary<Stat, float>();

    private void Awake()
    {
        playerStats = PlayerStatSO.BaseStats;
        foreach(KeyValuePair<Stat, float> kvp in playerStats)
        {
            addStats.Add(kvp.Key, 0f);
            addObjectStat.Add(kvp.Key, 0f);
        }
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

    internal void AddObject(Dictionary<Stat, float> objectStat)
    {
        foreach(KeyValuePair<Stat, float> kvp in objectStat)
        {
            addObjectStat[kvp.Key] += kvp.Value;
        }
        UpgradeStats();
    }

    public float GetStat(Stat stat)
    {
        return playerStats[stat] + addStats[stat] + addObjectStat[stat];
    }

    private void UpgradeStats()
    {
        IEnumerable<IPlayerStatDependency> listeners =
                    FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None).OfType<IPlayerStatDependency>();

        foreach (var listener in listeners)
        {
            listener.UpdateStat(this);
        }
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                UpgradeStats();
                break;
        }
    }

}

public interface IPlayerStatDependency
{
    void UpdateStat(PlayerStatManager playerStatManager);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsTab : MonoBehaviour, IPlayerStatDependency
{
    [SerializeField] private List<StatContainer> list;

    private void UpdateTab(PlayerStatManager playerStatManager)
    {
        Stat[] stats = (Stat[])Enum.GetValues(typeof(Stat)); //list.count = stats.leght
        for(int i = 0; i < list.Count; i++)
        {
            Sprite icon = GameAssets.LoadStatIcon(stats[i]);
            string name = Enums.FormatStatName(stats[i]);
            float value = playerStatManager.GetStat(stats[i]);

            list[i].Confingue(icon, name, value);
        }
    }

    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        UpdateTab(playerStatManager);
    }
}

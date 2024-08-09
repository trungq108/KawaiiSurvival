using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameAssets 
{
    static StatIconDataSO statIcon;

    public static Sprite LoadStatIcon(Stat stat)
    {
        if(statIcon == null)
        {
            //Assets / Resources / Data / StatIconSO.asset
            statIcon = Resources.Load<StatIconDataSO>("Data/StatIconSO");            
        }


        for (int i = 0; i < statIcon.StatIcons.Length; i++)
        {
            if(stat == statIcon.StatIcons[i].stat)
            {
                return statIcon.StatIcons[i].icon;
            }
        }

        Debug.LogError("Can't Find Icon with stat you want");
        return null; ;
    }
}

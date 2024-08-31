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

    static private ObjectDataSO[] ObjectDatas;
    static public ObjectDataSO[] LoadObjectDatas()
    {
        if(ObjectDatas == null)
        {
            // Assets/Resources/Data/Objects/
            ObjectDatas = Resources.LoadAll<ObjectDataSO>("Data/Objects/");
        }
        return ObjectDatas;
    }

    static public ObjectDataSO GetRandomObjectData()
    {
        if (ObjectDatas == null) LoadObjectDatas();
        return ObjectDatas[Random.Range(0, ObjectDatas.Length)];
    }


    static private WeaponDataSO[] WeaponDatas;
    static public WeaponDataSO[] LoadWeaponDatas()
    {
        if (WeaponDatas == null)
        {
            // Assets/Resources/Data/Weapons/
            WeaponDatas = Resources.LoadAll<WeaponDataSO>("Data/Weapons/");
        }
        return WeaponDatas;
    }

    static public WeaponDataSO GetRandomWeaponData()
    {
        if (WeaponDatas == null) LoadWeaponDatas();
        return WeaponDatas[Random.Range(0, WeaponDatas.Length)];
    }

    static private CharacterDataSO[] CharacterDatas;
    static public CharacterDataSO[] LoadCharacters()
    {
        if (CharacterDatas == null)
        {
            // Assets/Resources/Data/Characters/
            CharacterDatas = Resources.LoadAll<CharacterDataSO>("Data/Characters/");
        }
        return CharacterDatas;
    }
}
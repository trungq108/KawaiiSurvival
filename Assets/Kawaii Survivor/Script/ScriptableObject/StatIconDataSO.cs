using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = ("StatIconSO"), menuName = ("Scriptable Object/StatIcon"), order = 3)]
public class StatIconDataSO : ScriptableObject
{
    [field: SerializeField] public StatIcon[] StatIcons {  get; private set; }
}

[Serializable]
public struct StatIcon
{
    public Stat stat;
    public Sprite icon;
}

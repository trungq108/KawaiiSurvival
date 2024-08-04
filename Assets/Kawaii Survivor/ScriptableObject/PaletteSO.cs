using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaletteSO", menuName = "Scriptable Object / New Palette", order = 0)]
public class PaletteSO : ScriptableObject
{
    [field: SerializeField] public Color[] LevelColor { get; private set; }
    [field: SerializeField] public Color[] OutlineColor {  get; private set; }

}

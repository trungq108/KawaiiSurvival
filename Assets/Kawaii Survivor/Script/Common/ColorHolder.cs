using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolder : Singleton<ColorHolder>
{
    [SerializeField] PaletteSO palette;

    public static Color GetColor(int index)
    {
        index = Mathf.Clamp(index, 0, Instance.palette.LevelColor.Length);
        return Instance.palette.LevelColor[index];
    }

    public static Color GetOutlineColor(int index)
    {
        index = Mathf.Clamp(index, 0, Instance.palette.OutlineColor.Length);
        return Instance.palette.OutlineColor[index];
    }
}

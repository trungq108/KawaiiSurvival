using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action<Enemy, int> HitEnemy;
    public static Action CandyCollected;
    public static Action MoneyCollected;
    public static Action ChestCollected;
    public static Action<Weapon> OnWeaponMerge;

    public static Action<bool> OnSFXChange;
    public static Action<bool> OnMusicChange;

    public static Action<AudioClip> onRangeWeaponAttack;
    public static Action<AudioClip> onMeleeWeaponAttack;

}

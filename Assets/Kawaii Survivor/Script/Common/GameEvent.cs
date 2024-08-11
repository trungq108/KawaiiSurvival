using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action<Enemy, int> HitEnemy;
    public static Action CandyCollected;
    public static Action ChestCollected;
}

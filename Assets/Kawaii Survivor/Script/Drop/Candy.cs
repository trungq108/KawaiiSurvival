using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropItem
{
    public static Action onCollected;

    protected override void OnCollectec(Player player)
    {
        base.OnCollectec(player);
        onCollected?.Invoke();
    }
}

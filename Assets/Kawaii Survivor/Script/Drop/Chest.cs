using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : DropItem
{
    public override void Pick(Player player)
    {
        LeanPool.Despawn(this);
    }
}

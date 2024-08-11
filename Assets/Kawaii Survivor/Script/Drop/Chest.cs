using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : DropItem
{
    protected override void OnCollected()
    {
        base.OnCollected();
        GameEvent.ChestCollected?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : DropItem
{
    protected override void OnCollected()
    {
        base.OnCollected();
        GameEvent.MoneyCollected?.Invoke();
    }
}

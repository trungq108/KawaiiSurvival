using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropItem
{
    protected override void OnCollected()
    {
        GameEvent.CandyCollected?.Invoke();
    }
}

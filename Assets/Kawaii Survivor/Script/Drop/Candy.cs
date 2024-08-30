using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropItem
{
    public void OnPick(Transform playerPos)
    {
        StartCoroutine(Picked(playerPos));
    }

    protected IEnumerator Picked(Transform playerPos)
    {
        float timer = 0f;
        while (timer < 0.5)
        {
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, playerPos.position, timer);
            yield return null;
        }
        OnCollected();
        LeanPool.Despawn(this.gameObject);
    }

    protected override void OnCollected()
    {
        GameEvent.CandyCollected?.Invoke();
    }
}

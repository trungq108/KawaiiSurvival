using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public virtual void Pick(Player player)
    {
        StartCoroutine(Picked(player));
    }

    IEnumerator Picked(Player player)
    {
        float timer = 0f;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, player.transform.position, timer);
            yield return null;
        }

        LeanPool.Despawn(this.gameObject);
    }
}

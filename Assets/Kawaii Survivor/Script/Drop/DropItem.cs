using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    [SerializeField] protected int dropExp;

    public void Pick(Player player)
    {
        StartCoroutine(Picked(player));
    }

    protected IEnumerator Picked(Player player)
    {
        float timer = 0f;
        while (timer < 0.5)
        {
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, player.transform.position, timer);
            yield return null;
        }
        LeanPool.Despawn(this.gameObject);
        OnCollectec(player);
    }

    protected virtual void OnCollectec(Player player)
    {

    }
}

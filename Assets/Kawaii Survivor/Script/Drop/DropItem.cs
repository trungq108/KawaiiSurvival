using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    [SerializeField] protected int dropExp;

    public void OnPick(Player player)
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
        OnCollected();
        LeanPool.Despawn(this.gameObject);
    }

    protected virtual void OnCollected() { }

}

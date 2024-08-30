using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            OnCollected();
            LeanPool.Despawn(this.gameObject);
        }
    }

    protected virtual void OnCollected() { }

}

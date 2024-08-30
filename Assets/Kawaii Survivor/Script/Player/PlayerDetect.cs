using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField] private Collider2D detectCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouching(detectCollider))
        {
            if (collision.TryGetComponent(out Candy candy))
            {
                candy.OnPick(this.transform);
            }
        }
    }
}

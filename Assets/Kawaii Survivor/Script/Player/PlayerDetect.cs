using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField] private Collider2D detectCollider;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent(out DropItem dropItem))
        {
            Debug.Log("Picking");
            dropItem.Pick(player);
        }
    }
}

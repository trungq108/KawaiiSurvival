using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] float moveSpeed;

    private void Update()
    {
        if(player != null)
        {
            EnemyFollow(player.transform.position);
        }
    }
    public void EnemyFollow(Vector2 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * moveSpeed);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}

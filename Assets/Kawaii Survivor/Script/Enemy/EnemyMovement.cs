using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    private float distanceRange;

    [SerializeField] float moveSpeed;

    public void FollowPlayer()
    {
        if (player == null) return;
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceRange) return;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
    }

    public void OnInit(Player player, float distanceRange)
    {
        this.player = player;
        this.distanceRange = distanceRange;
    }
}

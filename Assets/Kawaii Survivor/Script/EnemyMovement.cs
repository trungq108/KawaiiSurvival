using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Element ")]
    private Player player;

    [Header(" Setting ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detechRange;

    [Header(" Setting ")]
    [SerializeField] bool isGizmos;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        if (player == null) Destroy(this.gameObject);
    }

    void Update()
    {
        EnemyFollow(player.transform.position);
        TryAttack(player.transform.position);
    }

    public void EnemyFollow(Vector2 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * moveSpeed);
    }

    private void TryAttack(Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);
        if (distance < detechRange)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detechRange);
    }
}

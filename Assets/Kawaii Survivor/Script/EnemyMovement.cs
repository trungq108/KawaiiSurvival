using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Element ")]
    private Player player;
    [SerializeField] SpriteRenderer enemyRenderer;
    [SerializeField] SpriteRenderer spawnCircle;

    [Header(" Setting ")]
    [SerializeField] float moveSpeed;
    [SerializeField] float detechRange;
    private bool isSpawned;

    [Header(" Visual ")]
    [SerializeField] GameObject bloodVFXPrefab;


    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        player = FindObjectOfType<Player>();
        if (player == null) Destroy(this.gameObject);

        isSpawned = false;
        enemyRenderer.enabled = false;
        spawnCircle.enabled = true;

        spawnCircle.transform.DOScale(spawnCircle.transform.localScale * 1.2f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(3)
            .OnComplete(EnemyActivace);
    }

    private void EnemyActivace()
    {
        isSpawned = true;
        enemyRenderer.enabled = true;
        spawnCircle.enabled = false;
    }

    void Update()
    {
        if(!isSpawned) return;
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
            Dead();
        }
    }

    private void Dead()
    {
        GameObject bloodVFX = LeanPool.Spawn(bloodVFXPrefab, this.transform.position, Quaternion.identity);
        LeanPool.Despawn(bloodVFX, 2f);
        Destroy(this.gameObject);
    }

}

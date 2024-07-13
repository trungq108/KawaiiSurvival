using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private EnemyMovement enemyMovement;
    [SerializeField] SpriteRenderer enemyRenderer;
    [SerializeField] SpriteRenderer spawnCircle;

    [SerializeField] float detectRange;
    [SerializeField] float attackDuration;
    private float attackTimer;
    private bool isSpawned;

    [SerializeField] GameObject bloodVFXPrefab;


    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        player = FindObjectOfType<Player>();
        if (player == null) Destroy(this.gameObject);
        enemyMovement = GetComponent<EnemyMovement>();

        isSpawned = false;
        enemyRenderer.enabled = false;
        spawnCircle.enabled = true;

        spawnCircle.transform.DOScale(spawnCircle.transform.localScale * 1.4f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(3)
            .OnComplete(EnemyActivace);
    }

    private void EnemyActivace()
    {
        isSpawned = true;
        enemyRenderer.enabled = true;
        spawnCircle.enabled = false;
        enemyMovement.SetPlayer(player);
    }

    void Update()
    {
        if (!isSpawned) return;
        if (attackTimer >= attackDuration)
        {
            TryAttack(player.transform.position);
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    private void TryAttack(Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);
        if (distance < detectRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        attackTimer = 0f;
    }

    private void DestroyEnemy()
    {
        GameObject bloodVFX = LeanPool.Spawn(bloodVFXPrefab, this.transform.position, Quaternion.identity);
        LeanPool.Despawn(bloodVFX, 2f);
        LeanPool.Despawn(this.gameObject);
    }

}

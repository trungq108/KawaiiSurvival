using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Player player;
    protected EnemyMovement enemyMovement;
    protected EnemyHealth enemyHealth;
    protected Collider2D collider;

    [Header("Spawn")]
    [SerializeField] SpriteRenderer enemyRenderer;
    [SerializeField] SpriteRenderer spawnCircle;

    [Header("AttackSetting")]
    [SerializeField] protected float detectRange;
    [SerializeField] protected float attackDuration;
    [SerializeField] protected int attackDamage;
    protected float attackTimer;
    protected bool isSpawned;

    protected void Start()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        player = GameManager.Instance.Player;
        if (player == null)
        {
            Destroy(this.gameObject);
            Debug.Log("Don't Find Player");
        }
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        collider = GetComponent<Collider2D>();
        collider.enabled = false;

        isSpawned = false;
        enemyRenderer.enabled = false;
        spawnCircle.enabled = true;
        enemyHealth.OnInit();

        spawnCircle.transform.DOScale(spawnCircle.transform.localScale * 1.4f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(3)
            .OnComplete(OnInitCompleted);
    }

    protected virtual void OnInitCompleted()
    {
        isSpawned = true;
        enemyRenderer.enabled = true;
        spawnCircle.enabled = false;
        collider.enabled = true;
        enemyHealth.OnInitCompleted();
        enemyMovement.OnInit(player, detectRange);
    }

    protected virtual void Update()
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
        enemyMovement.FollowPlayer();
    }

    protected virtual void TryAttack(Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);
        if (distance <= detectRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        attackTimer = 0f;
    }

    public virtual void TakeDamage(int damage, bool isCritical)
    {
        enemyHealth.TakeDame(damage, isCritical);
        GameEvent.HitEnemy?.Invoke(this, damage);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}

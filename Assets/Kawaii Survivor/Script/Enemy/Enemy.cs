using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;
    private Collider2D collider;

    [Header("Spawn")]
    [SerializeField] SpriteRenderer enemyRenderer;
    [SerializeField] SpriteRenderer spawnCircle;

    [Header("AttackSetting")]
    [SerializeField] float detectRange;
    [SerializeField] float attackDuration;
    [SerializeField] int attackDamage;
    private float attackTimer;
    private bool isSpawned;

    [Header("VFX")]
    [SerializeField] GameObject bloodVFXPrefab;
    [SerializeField] DamageText damageText;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        player = GameManager.Instance.Player;
        if (player == null) Destroy(this.gameObject);
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        collider = GetComponent<Collider2D>();
        collider.enabled = false;


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
        collider.enabled = true;
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
        player.TakeDamage(attackDamage);
        attackTimer = 0f;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.TakeDame(damage);

        DamageText textDamage = LeanPool.Spawn(damageText, this.transform.position, Quaternion.identity); 
        textDamage.Trigger(damage);
        LeanPool.Despawn(textDamage, 1f);

        GameObject bloodVFX = LeanPool.Spawn(bloodVFXPrefab, this.transform.position, Quaternion.identity); 
        LeanPool.Despawn(bloodVFX, 2f);

    }
}

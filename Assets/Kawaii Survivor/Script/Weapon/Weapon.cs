using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] GameObject parent;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D collider;
    private string currentAnim = "idle";
    private Enemy nearestTarget;

    [Header("Setting")]
    [SerializeField] int weaponDamage;
    [SerializeField] float aimSpeed;
    [SerializeField] float detectRadius;
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] float attackDelay;
    private float timer;

    private void Start()
    {
        InvokeRepeating(nameof(FindNearestTarget), 0f, 0.5f);
    }

    void Update()
    {
        AimTarget();

        timer += Time.deltaTime;
        if (timer > attackDelay && nearestTarget != null) Attack();
    }

    private void FindNearestTarget()
    {
        float min = float.MaxValue;
        Enemy nearestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(parent.transform.position, detectRadius, enemyLayerMask);
        for(int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(parent.transform.position, enemies[i].transform.position);
            if (distance < min)
            {
                nearestEnemy = enemies[i].GetComponent<Enemy>();
                min = distance;
            }
        }
        nearestTarget = nearestEnemy;
    }

    private void AimTarget()
    {
        Vector3 targetDirection;

        if(nearestTarget == null)
        {
            targetDirection = Vector3.up;
        }
        else
        {
            targetDirection = (nearestTarget.transform.position - parent.transform.position).normalized;
        }
        parent.transform.up = Vector3.Lerp(parent.transform.up, targetDirection, Time.deltaTime * aimSpeed);
    }

    private void Attack()
    {
        ChangAnim("attack");
        collider.enabled = true;
        timer = 0f;
    }

    private void StopAttack()
    {
        ChangAnim("idle");
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.GetComponent<Enemy>().TakeDamage(weaponDamage);
        }
    }

    private void ChangAnim(string nextAnim)
    {
        if(currentAnim != nextAnim)
        {
            animator.ResetTrigger(nextAnim);
            currentAnim = nextAnim;
            animator.SetTrigger(currentAnim);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(parent.transform.position, detectRadius);
    }

}

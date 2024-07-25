using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] protected GameObject parent;
    [SerializeField] protected Animator animator;
    protected string currentAnim = "idle";
    protected Enemy nearestTarget;

    [Header("Setting")]
    [SerializeField] protected int weaponDamage;
    [SerializeField] protected float aimSpeed;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float detectDelay;
    protected float timer;

    protected void OnEnable()
    {
        InvokeRepeating(nameof(FindNearestTarget), 0f, detectDelay);
    }

    protected virtual void Update()
    {
        AimTarget();

        timer += Time.deltaTime;
        if (timer > attackDelay && nearestTarget != null) Attack();
    }

    protected void FindNearestTarget()
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

    protected void AimTarget()
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

    protected virtual void Attack()
    {
        ChangAnim("attack");
        timer = 0f;
    }

    protected virtual void StopAttack()
    {
        ChangAnim("idle");
    }

    public int GetDamage(out bool isCritical)
    {
        isCritical = false;
        int damage = this.weaponDamage;
        if (Random.Range(0, 100) < 20)
        {
            isCritical = true;
            damage = this.weaponDamage * 2;
        }
        return damage;
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

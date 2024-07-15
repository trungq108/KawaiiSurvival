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
    private string currentAnim = "idle";
    private Enemy nearestTarget;

    [Header("Setting")]
    [SerializeField] int weaponDamage;
    [SerializeField] float aimSpeed;
    [SerializeField] float detectRadius;
    [SerializeField] LayerMask enemyLayerMask;

    [SerializeField] Button test1;
    [SerializeField] Button test2;

    private void Awake()
    {
        test1.onClick.AddListener(() => Attack());
        test2.onClick.AddListener(() => StopAttack());

    }

    private void Start()
    {
        InvokeRepeating(nameof(FindNearestTarget), 0f, 0.5f);
        InvokeRepeating(nameof(Attack), 0f, 1f);
    }

    void Update()
    {
        AimTarget();
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
    }

    private void StopAttack()
    {
        ChangAnim("idle");
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

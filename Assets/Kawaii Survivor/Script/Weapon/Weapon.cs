using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Enemy nearestTarget;
    [SerializeField] float aimSpeed;
    [SerializeField] float detectRadius;
    [SerializeField] LayerMask enemyLayerMask;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InvokeRepeating(nameof(FindNearestTarget), 0f, 0.5f);
    }

    void Update()
    {
        AimTarget();
    }

    private void FindNearestTarget()
    {
        float min = float.MaxValue;
        Enemy nearestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, enemyLayerMask);
        for(int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
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
            targetDirection = (nearestTarget.transform.position - transform.position).normalized;
        }
        transform.up = Vector3.Lerp(targetDirection, targetDirection, Time.deltaTime * aimSpeed);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}

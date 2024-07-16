using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] Bullet bulletPrefab;

    protected override void Attack()
    {
        ShotCommand(player.transform.position);
        attackTimer = 0f;
    }

    private void ShotCommand(Vector3 pos)
    {
        Vector2 direction = (pos - shotPoint.transform.position).normalized;
        Bullet bullet = LeanPool.Spawn(bulletPrefab, shotPoint.transform.position, Quaternion.identity);
        bullet.GetDirection(direction, attackDamage);
        LeanPool.Despawn(bullet, 3f);
    }
}

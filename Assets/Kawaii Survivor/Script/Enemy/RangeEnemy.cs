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
        base.Attack();
        OnShooting(player.transform.position);
    }

    private void OnShooting(Vector3 pos)
    {
        Vector2 direction = (pos - shotPoint.transform.position).normalized;
        Bullet bullet = LeanPool.Spawn(bulletPrefab, shotPoint.transform.position, Quaternion.identity);
        bullet.Shoot(direction, attackDamage, false, false);
        LeanPool.Despawn(bullet, 3f);
    }
}

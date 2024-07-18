using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RangeWeapon : Weapon
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;

    protected override void Attack()
    {
        base.Attack();
        Shooting(nearestTarget.transform.position);
    }

    protected override void StopAttack()
    {
        base.StopAttack();
    }

    private void Shooting(Vector3 pos)
    {
        Vector2 direction = (pos - shootingPoint.transform.position).normalized;
        Bullet bullet = LeanPool.Spawn(bulletPrefab, shootingPoint.transform.position, Quaternion.identity);
        bullet.Shoot(direction, weaponDamage);
        LeanPool.Despawn(bullet, 3f);
    }
}

using Lean.Pool;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;

    protected override void Attack()
    {
        base.Attack();
        Shooting(nearestTarget.transform.position);
        GameEvent.onRangeWeaponAttack?.Invoke(Data.WeaponSound);
    }

    protected override void StopAttack()
    {
        base.StopAttack();
    }

    private void Shooting(Vector3 pos)
    {
        Vector2 direction = (pos - shootingPoint.transform.position).normalized;
        int damage = GetDamage(out bool isCritical);

        Bullet bullet = LeanPool.Spawn(bulletPrefab, shootingPoint.transform.position, Quaternion.identity);
        bullet.Shoot(direction, damage, isCritical, true);
        LeanPool.Despawn(bullet, 3f);
    }
}

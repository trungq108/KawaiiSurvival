using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Collider2D collider;

    protected override void Attack()
    {
        base.Attack();
        collider.enabled = true;
    }

    protected override void StopAttack()
    {
        base.StopAttack();
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            int damage = GetDamage(out bool isCritical);
            collision.GetComponent<Enemy>().TakeDamage(damage, isCritical);
        }
    }

}

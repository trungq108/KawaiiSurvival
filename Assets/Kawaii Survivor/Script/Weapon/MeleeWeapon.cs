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

}

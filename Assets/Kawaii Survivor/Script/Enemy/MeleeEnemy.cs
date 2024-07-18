using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Attack()
    {
        base.Attack();
        player.TakeDamage(attackDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour, IPlayerStatDependency
{
    [Header("Element")]
    [SerializeField] protected GameObject parent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected WeaponDataSO data;
    protected string currentAnim = "idle";
    protected Enemy nearestTarget;

    [Header("Setting")]
    [SerializeField] protected float aimSpeed;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float detectDelay;
    protected float timer;

    protected int weaponLevel;
    protected float attackSpeed;
    protected float damage;
    protected float criticalChance;
    protected float criticalPercent;
    protected float attackRange;



    protected void OnEnable()
    {
        InvokeRepeating(nameof(FindNearestTarget), 0f, detectDelay);
    }

    protected virtual void Update()
    {
        AimTarget();

        timer += Time.deltaTime;
        if (timer > 1 / attackSpeed && nearestTarget != null) Attack();
    }

    protected void FindNearestTarget()
    {
        float min = float.MaxValue;
        Enemy nearestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(parent.transform.position, attackRange, enemyLayerMask);
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
        int damage = Mathf.RoundToInt(this.damage);
        if (Random.Range(0, 100) < criticalChance)
        {
            isCritical = true;
            damage = Mathf.RoundToInt(this.damage * (1 + criticalPercent / 100));
        }

        return damage;
    }

    protected void ConfigueStat() //Weapon Pure Data Update per Level
    {
        float multiple = 1 + ((weaponLevel -1) / 3);
        attackSpeed = data.GetStat(Stat.AttackSpeed) * multiple;
        damage = data.GetStat(Stat.Attack) * multiple;
        criticalChance = data.GetStat(Stat.CriticalChance) * multiple;
        criticalPercent = data.GetStat(Stat.CriticalPercent) * multiple;
        if (this.GetType() == typeof(RangeWeapon))
        {
            attackRange = data.GetStat(Stat.Range) * multiple;
        }
        else if (this.GetType() == typeof(MeleeWeapon)) 
        {
            attackRange = 5;
        }
    }

    public void UpdateStat(PlayerStatManager playerStatManager) //Weapon Data Update with Player Data
    {
        ConfigueStat();
        attackSpeed *= (1 + playerStatManager.GetStat(Stat.AttackSpeed) / 100 ); attackSpeed = Mathf.Max(attackSpeed, 0.1f);
        damage *= (1 + playerStatManager.GetStat(Stat.Attack) / 100);
        criticalChance *= (1 + playerStatManager.GetStat(Stat.CriticalChance) / 100);
        criticalPercent *= (1 + playerStatManager.GetStat(Stat.CriticalPercent) / 100);
        if (this.GetType() == typeof(RangeWeapon))
        {
            attackRange *= (1 + playerStatManager.GetStat(Stat.CriticalPercent) / 100);
        }
    
    }

    private void ChangAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            animator.ResetTrigger(nextAnim);
            currentAnim = nextAnim;
            animator.SetTrigger(currentAnim);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(parent.transform.position, attackRange);
    }

}

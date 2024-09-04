using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bossii : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] Bullet bulletPrefab;

    private string currentAnimName = "";
    public IState currentState;
    [HideInInspector] public int ShotRotateMagnitude = 0;
    public Vector2 ShotDirection { get; private set; }

    protected override void OnInit()
    {
        base.OnInit();
        currentState = new IdleState();
    }

    protected override void Update()
    {
        if (!isSpawned) return;
        currentState.OnExcute(this);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null) currentState.OnExit(this);
        currentState = newState;
        if (currentState != null) currentState.OnEnter(this);
    }

    public void Moving(Vector3 moveDirection)
    {
        transform.position = Vector2.MoveTowards(transform.position, moveDirection, 5f * Time.deltaTime);
    }

    protected override void Attack()
    {
        ShotDirection = Quaternion.Euler(0, 0, 45 * ShotRotateMagnitude) * Vector2.up;
        Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Shoot(ShotDirection, attackDamage, false, false);
        LeanPool.Despawn(bullet, 3f);
        ShotRotateMagnitude++;
    }

    private void ChangeToIdle() => ChangeState(new IdleState());

    public Vector2 GetRandomPosition()
    {
        Vector2 pos = new Vector2();

        pos.x = Random.Range(-25f, 25f);
        pos.y = Random.Range(-12f, 12f);
        pos.x = Mathf.Clamp(pos.x, -25, 25);  //Map right-left bound
        pos.y = Mathf.Clamp(pos.y, -12, 12);  //Map up-down bound

        return pos;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
}

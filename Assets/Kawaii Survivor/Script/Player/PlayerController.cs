using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerStatDependency
{

    private float moveSpeed;
    private Rigidbody2D playerRb;
    [SerializeField] Animator animator;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MobileJoystick.Instance == null) return;

        if (MobileJoystick.Instance.GetMoveVector().magnitude > 0.1f)
        {
            playerRb.velocity = MobileJoystick.Instance.GetMoveVector() * moveSpeed * Time.deltaTime;
            animator.Play("Move");
        }
        else
        {
            playerRb.velocity = Vector3.zero;
            animator.Play("Idle");
        }
    }
    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        moveSpeed = playerStatManager.GetStat(Stat.MoveSpeed);
    }

}

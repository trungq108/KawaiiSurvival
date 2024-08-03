using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerStatDependency
{

    public float moveSpeed;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MobileJoystick.Instance != null)
        {
            playerRb.velocity = MobileJoystick.Instance.GetMoveVector() * moveSpeed * Time.deltaTime;
        }
        else
        {
            playerRb.velocity = Vector3.zero;
        }
    }
    public void UpdateStat(PlayerStatManager playerStatManager)
    {
        moveSpeed = playerStatManager.GetStat(Stat.MoveSpeed);
    }

}

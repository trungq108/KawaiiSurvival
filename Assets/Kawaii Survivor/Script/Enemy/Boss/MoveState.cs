using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    Vector2 moveDirection;

    public void OnEnter(Bossii bot)
    {
        bot.ChangeAnim("move");
        moveDirection = bot.GetRandomPosition();
    }

    public void OnExcute(Bossii bot)
    {
        bot.Moving(moveDirection);
        if(Vector2.Distance(bot.transform.position, moveDirection) < 0.1f)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bossii bot)
    {

    }
}

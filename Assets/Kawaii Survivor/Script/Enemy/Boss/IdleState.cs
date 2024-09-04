using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer = 0;
    float idleTime = 2f;

    public void OnEnter(Bossii bot)
    {
        bot.ChangeAnim("idle");
    }

    public void OnExcute(Bossii bot)
    {
        timer += Time.deltaTime;
        if(timer > idleTime)
        {
            bot.ChangeState(new MoveState());
        }
    }

    public void OnExit(Bossii bot)
    {

    }
}

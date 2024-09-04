using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Bossii bot)
    {
        bot.ShotRotateMagnitude = 0;
        bot.ChangeAnim("attack");
    }

    public void OnExcute(Bossii bot)
    {

    }

    public void OnExit(Bossii bot)
    {

    }
}

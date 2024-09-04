using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(Bossii bot);
    public void OnExcute(Bossii bot);
    public void OnExit(Bossii bot);
}

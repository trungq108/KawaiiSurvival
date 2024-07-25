using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    private Player player; public Player Player => player; 
    void Awake()
    {
        Application.targetFrameRate = 60;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }
}

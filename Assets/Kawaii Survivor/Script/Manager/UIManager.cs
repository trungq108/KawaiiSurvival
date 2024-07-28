using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IGameStateListener
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject waveTransitionPanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject weaponSelectionPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject stageCompletePanel;
    private List<GameObject> panelList = new List<GameObject>();

    private void Awake()
    {
        panelList.AddRange(new GameObject[]
        {
            gamePanel, menuPanel, waveTransitionPanel, shopPanel , weaponSelectionPanel, gameOverPanel, stageCompletePanel
            , 
        });
    }


    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.WAVETRANSITION:
                ShowPanel(waveTransitionPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
            case GameState.WEAPONSELECTION:
                ShowPanel(weaponSelectionPanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameOverPanel);
                break;
            case GameState.STAGECOMPLETE:
                ShowPanel(stageCompletePanel);
                break;
        }
    }

    public void ShowPanel(GameObject panel)
    {
        for(int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetActive(panelList[i] == panel);
        }

    }
}

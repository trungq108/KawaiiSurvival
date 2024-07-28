using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Button menuToWeaponSelection;
    [SerializeField] Button shopToGame;
    [SerializeField] Button enterShop;
    [SerializeField] Button weaponSelectionToGame;
    [SerializeField] Button reloadStage;
    [SerializeField] Button playAgain;

    private Player player; public Player Player => player;

    private void OnEnable()
    {
        menuToWeaponSelection.onClick.AddListener(() => SetGameState(GameState.WEAPONSELECTION));
        enterShop.onClick.AddListener(()             => SetGameState(GameState.SHOP));
        shopToGame.onClick.AddListener(()            => SetGameState(GameState.GAME));
        weaponSelectionToGame.onClick.AddListener(() => SetGameState(GameState.GAME));
        reloadStage.onClick.AddListener(()           => LoadScene());
        playAgain.onClick.AddListener(()             => LoadScene());
    }

    void Awake()
    {
        player = FindObjectOfType<Player>();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SetGameState(GameState.MENU);  
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> listeners = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (var listener in listeners)
        {
            listener.GameStateChangeCallBack(gameState);
        }
    }

    public void WaveCompleteCallback()
    {
        if (player.HasLevelUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}

public interface IGameStateListener
{
    void GameStateChangeCallBack(GameState gameState);
}

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
    [SerializeField] Button weaponSelectionToGame;
    [SerializeField] Button gameover_Replay;
    [SerializeField] Button stageComplete_Replay;
    [SerializeField] Button pauseGame;
    [SerializeField] Button charaterSeclection;
    [SerializeField] Button backToMenu;

    public Player Player { get; private set; }
    public static bool IsPause {  get; private set; }

    private void OnEnable()
    {
        menuToWeaponSelection.onClick.AddListener(() => SetGameState(GameState.WEAPONSELECTION));
        shopToGame.onClick.AddListener(()            => SetGameState(GameState.GAME));
        weaponSelectionToGame.onClick.AddListener(() => SetGameState(GameState.GAME));
        gameover_Replay.onClick.AddListener(()       => LoadScene());
        stageComplete_Replay.onClick.AddListener(()  => LoadScene());
        pauseGame.onClick.AddListener(()             => PauseGame());
        charaterSeclection.onClick.AddListener(()    => SetGameState(GameState.CHARACTERSELECTION));
        backToMenu.onClick.AddListener(()            => SetGameState(GameState.MENU));

    }

    void Awake()
    {
        Player = FindObjectOfType<Player>();
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
        if (Player.HasLevelUp() || WaveTransition.Instance.ChestCollected > 0)
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void LoadScene() => SceneManager.LoadScene(0);
    public void LoadFromPause()
    {
        Time.timeScale = 1.0f;
        IsPause = false;
        LoadScene();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.PAUSE);
        IsPause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        SetGameState(GameState.GAME);
        IsPause = false;
    }

}

using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour, IGameStateListener
{
    private float waveDuration = 20f;
    [SerializeField] private Wave[] waves;

    private float timer = 0f;
    private int currentWaveIndex = 0;
    private List<float> localCount = new List<float>();
    private bool IsTimeOn;
    private WaveManagerUI ui;

    private void Awake()
    {
        ui = GetComponent<WaveManagerUI>();
    }

    private void Update()
    {
        if (!IsTimeOn) return;
        if (timer < waveDuration)
        {
            ManageCurrentWave();
            string timerString = ((int)(waveDuration - timer)).ToString();
            ui.UpdateWaveTimerText(timerString);
        }
        else
        {
            WaveTransition();
        }
    }

    private void StartWave(int waveIndex)
    {
        ui.UpdateWaveText("Wave " + (currentWaveIndex + 1).ToString() + " / " + waves.Length);
        localCount.Clear();
        foreach (WaveSegment segment in waves[currentWaveIndex].segments)
        {
            localCount.Add(1);
        }
        timer = 0f;
        IsTimeOn = true;
    }

    public void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];
            float timeStart = segment.tStartEnd.x / 100 * waveDuration;
            float timeEnd = segment.tStartEnd.y / 100 * waveDuration;

            if(timer < timeStart && timer > timeEnd) continue;

            float timerSinceSegmentStart = timer - timeStart;
            if (timerSinceSegmentStart * segment.spawnFrequency > localCount[i])
            {
                LeanPool.Spawn(segment.enemyPrefab, GetSpawnPos(), Quaternion.identity, this.transform);
                localCount[i]++;
            }
        }
        timer += Time.deltaTime;
    }

    private void WaveTransition()
    {
        ClearWave();
        IsTimeOn = false;
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Length)
        {
            ui.UpdateWaveText("Wave End !");
            GameManager.Instance.SetGameState(GameState.STAGECOMPLETE);
        }
        else
        {
            GameManager.Instance.WaveCompleteCallback();
        }
    }

    private void ClearWave()
    {
        transform.Clear();
    }

    private Vector2 GetSpawnPos()
    {
        Vector2 offset = Random.insideUnitSphere.normalized * 10;
        Vector2 playerPos = GameManager.Instance.Player.transform.position;
        Vector2 pos = playerPos + offset;
        pos.x = Mathf.Clamp(pos.x, -27, 27);  //Map right-left bound
        pos.y = Mathf.Clamp(pos.y, -15, 15);  //Map up-down bound
        return pos;
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                StartWave(currentWaveIndex);
                break;
            case GameState.GAMEOVER:
                ClearWave();
                break;
        }
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[System.Serializable]
public struct WaveSegment
{
    public Vector2 tStartEnd; // sliderVector x: timeStart , y : timeEnd
    public float spawnFrequency;
    public GameObject enemyPrefab;
}
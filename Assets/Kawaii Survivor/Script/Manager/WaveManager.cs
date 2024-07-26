using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] float waveDuration;
    [SerializeField] Wave[] waves;

    private float timer = 0f;
    private int currentWaveIndex = 0;
    private List<float> localCount = new List<float>();
    private Player player;

    private WaveManagerUI ui;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        ui = GetComponent<WaveManagerUI>();        
    }

    private void Start()
    {
        StartWave(currentWaveIndex);
    }

    private void Update()
    {
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
        localCount.Clear();
        foreach (WaveSegment segment in waves[currentWaveIndex].segments)
        {
            localCount.Add(1);
        }
        timer = 0f;
        ui.UpdateWaveText("Wave " + currentWaveIndex + " / " + waves.Length);
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
        //ClearWave();
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            StartWave(currentWaveIndex);
        }
        else
        {
            ui.UpdateWaveText("Wave End !");
        }
    }

    private void ClearWave()
    {
        transform.Clear();
    }

    private Vector2 GetSpawnPos()
    {
        Vector2 offset = Random.insideUnitSphere.normalized * 10;
        Vector2 pos = (Vector2)player.transform.position + offset;
        pos.x = Mathf.Clamp(pos.x, -27, 27);  //Map right-left bound
        pos.y = Mathf.Clamp(pos.y, -15, 15);  //Map up-down bound
        return pos;
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
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] float waveDuration;
    [SerializeField] Wave[] waves;

    private float timer = 0f;
    private List<float> localCount = new List<float>();
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;
        localCount.Add(1);
        localCount.Add(1);
    }

    private void Update()
    {
        if (timer < waveDuration)
        {
            ManageCurrentWave();
        }
    }

    public void ManageCurrentWave()
    {
        Wave currentWave = waves[0];
        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];
            float timeStart = segment.tStartEnd.x / 100 * waveDuration;
            float timeEnd = segment.tStartEnd.y / 100 * waveDuration;

            if(timer < timeStart && timer > timeEnd) continue;

            float timerSinceSegmentStart = timer - timeStart;
            if (timerSinceSegmentStart * segment.spawnFrequency > localCount[i])
            {
                LeanPool.Spawn(segment.enemyPrefab, GetSpawnPos(), Quaternion.identity);
                localCount[i]++;
            }
        }
        timer += Time.deltaTime;
    }

    private Vector2 GetSpawnPos()
    {
        Vector2 offset = Random.insideUnitSphere.normalized * 10;
        Vector2 pos = (Vector2)player.transform.position + offset;
        offset.x = Mathf.Clamp(offset.x, -27, 27);
        offset.y = Mathf.Clamp(offset.y, -15, 15);
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
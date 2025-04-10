using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float spawnRate;
    }

    public Wave[] waves;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                currentWaveIndex++;
            }
            yield return null;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        isSpawning = false;

        // Optional: Wait a bit before next wave starts
        yield return new WaitForSeconds(5f);
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
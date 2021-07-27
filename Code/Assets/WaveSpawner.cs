using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f; // between each waves
    private float countdown = 2f; // countdown before the 1st wave
    public Text WaveCountdownTimerText;
    
    private int waveIndex = 0;
    public float timeBetweenEachEnemySpawn = 0.5f;

    void Awake()
    {
        Assert.IsNotNull(enemyPrefab);
        Assert.IsNotNull(spawnPoint);
        Assert.IsNotNull(WaveCountdownTimerText);
    }

    void Update()
    {
        if(countdown <= 0f) // when the 1st wave needs to be spawned
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        WaveCountdownTimerText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log("Wave incomming");
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEachEnemySpawn);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
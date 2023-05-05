using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public int fastCount;
        public int heavyCount;
    }

    #region Enemy Properties
    public GameObject enemyPrefab;
    public GameObject fastPrefab;
    public GameObject heavyPrefab;
    public Transform startPoint;
    public Transform endPoint;
    public List<Wave> waves;
    public float spawnEnemyInterval = 1.0f;
    public float spawnFastInterval = 0.5f;
    public float spawnHeavyInterval = 3f;
    public float spawnWaveInterval = 20.0f;
    public float[] levelDifficulties = { 1f, 1.5f, 2f, 4f };

    public TextMeshProUGUI  levelAndWavesText;
    public TextMeshProUGUI  nextWaveText;
    public TextMeshProUGUI  timerText;

    public int level = 0;
    public int currentWaveIndex = 0;
    public int timeToNextWave = 0;
    private float elapsedTime = 0f;

    private float elapsedTimeSinceLastWave;
    #endregion

    void Start()
    {
        elapsedTimeSinceLastWave = 10f;

        Wave wave1 = new Wave { enemyCount = 6, fastCount = 0, heavyCount = 0 };
        Wave wave2 = new Wave { enemyCount = 12, fastCount = 0, heavyCount = 0 };
        Wave wave3 = new Wave { enemyCount = 0, fastCount = 12, heavyCount = 0 };
        Wave wave4 = new Wave { enemyCount = 12, fastCount = 24, heavyCount = 0 };
        Wave wave5 = new Wave { enemyCount = 0, fastCount = 0, heavyCount = 3 };
        Wave wave6 = new Wave { enemyCount = 12, fastCount = 0, heavyCount = 3 };
        Wave wave7 = new Wave { enemyCount = 12, fastCount = 24, heavyCount = 3 };
        Wave wave8 = new Wave { enemyCount = 24, fastCount = 48, heavyCount = 6 };
        Wave wave9 = new Wave { enemyCount = 36, fastCount = 72, heavyCount = 9 };
        Wave wave10 = new Wave { enemyCount = 48, fastCount = 96, heavyCount = 12 };

        waves.Add(wave1);
        waves.Add(wave2);
        waves.Add(wave3);
        waves.Add(wave4);
        waves.Add(wave5);
        waves.Add(wave6);
        waves.Add(wave7);
        waves.Add(wave8);
        waves.Add(wave9);
        waves.Add(wave10);

        StartCoroutine(SpawnWaveRoutine());
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTimeSinceLastWave < 20f){
            elapsedTimeSinceLastWave += Time.deltaTime;
        }
        else{
            elapsedTimeSinceLastWave = 20f;
        }
        
        timeToNextWave = Mathf.CeilToInt(spawnWaveInterval - elapsedTimeSinceLastWave);

        levelAndWavesText.text = $"Level: {level + 1} Wave: {currentWaveIndex + 1}";
        nextWaveText.text = $"Next Wave: {timeToNextWave}s";
        timerText.text = FormatElapsedTime(elapsedTime);
    }

    private IEnumerator SpawnWaveRoutine()
    {
        yield return new WaitForSeconds(10f);
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            // Start spawn routines for each enemy type concurrently
            Coroutine enemyRoutine = StartCoroutine(SpawnEnemiesOfType(enemyPrefab, currentWave.enemyCount * levelDifficulties[level], spawnEnemyInterval * 1.5f / levelDifficulties[level]));
            Coroutine fastRoutine = StartCoroutine(SpawnEnemiesOfType(fastPrefab, currentWave.fastCount * levelDifficulties[level], spawnFastInterval * 1.5f / levelDifficulties[level]));
            Coroutine heavyRoutine = StartCoroutine(SpawnEnemiesOfType(heavyPrefab, currentWave.heavyCount * levelDifficulties[level], spawnHeavyInterval * 1.5f / levelDifficulties[level]));

            // Wait for all enemy types spawn routines to complete
            yield return enemyRoutine;
            yield return fastRoutine;
            yield return heavyRoutine;

            elapsedTimeSinceLastWave = 0f;

            yield return new WaitForSeconds(spawnWaveInterval);
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnEnemiesOfType(GameObject prefab, float count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            SpawnEnemy(prefab);
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        GameObject newEnemy = Instantiate(prefab, startPoint.position, Quaternion.identity);
        newEnemy.GetComponent<CharacterMovements>().endpoint = endPoint;
    }

    private string FormatElapsedTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}

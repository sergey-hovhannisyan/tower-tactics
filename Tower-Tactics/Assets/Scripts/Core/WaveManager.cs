using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public int fastCount;
        public int heavyCount;
        public int enemySpawned = 0;
        public int fastSpawned = 0;
        public int heavySpawned = 0;
    }

    #region  Enemy Properties
    public GameObject enemyPrefab;
    public GameObject fastPrefab;
    public GameObject heavyPrefab;
    public Transform startpoint;
    public Transform endpoint;
    public float spawnEnemyInterval = 0.5f;
    public float spawnWaveInterval = 15.0f;
    public List<Wave> waves;

    private float elapsedTime;
    private int enemiesSpawnedInCurrentWave;
    private int currentWave;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        enemiesSpawnedInCurrentWave = 0;
        currentWave = 0;
        StartCoroutine(SpawnEnemyRoutine());
    }

    #region Waves Control Methods
    // Spawns a wave of enemies
    private IEnumerator SpawnEnemyRoutine()
    {
        while (currentWave < waves.Count)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= spawnEnemyInterval)
            {
                elapsedTime = 0f;

                if (enemiesSpawnedInCurrentWave < waves[currentWave].enemyCount + waves[currentWave].fastCount + waves[currentWave].heavyCount)
                {
                    SpawnEnemy();
                    enemiesSpawnedInCurrentWave++;
                }
                else
                {
                    yield return new WaitForSeconds(spawnWaveInterval);
                    enemiesSpawnedInCurrentWave = 0;
                    currentWave++;
                }
            }
            yield return null;
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = null;

        
        if (waves[currentWave].fastCount > waves[currentWave].fastSpawned)
        {
            prefabToSpawn = fastPrefab;
            waves[currentWave].fastSpawned++;
        }
        else if (waves[currentWave].enemyCount > waves[currentWave].enemySpawned)
        {
            prefabToSpawn = enemyPrefab;
            waves[currentWave].enemySpawned++;
        }
        else if (waves[currentWave].heavyCount > waves[currentWave].heavySpawned)
        {
            prefabToSpawn = heavyPrefab;
            waves[currentWave].heavySpawned++;
        }

        if (prefabToSpawn != null)
        {
            GameObject newEnemy = Instantiate(prefabToSpawn, startpoint.position, Quaternion.identity);
            newEnemy.GetComponent<CharacterMovements>().endpoint = endpoint;
        }
    }

    #endregion
}

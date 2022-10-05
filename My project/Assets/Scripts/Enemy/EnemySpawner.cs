using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Attributes")]
    [SerializeField] private int maxEnemies;
    private int currentEnemies;
    [SerializeField] private float timeBetweenSpawns;
    private float timerSpawnRate;

    [Header("Required Components")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform spawnLocation;
    private bool spawnGateActive = false;

    private void Start()
    {
        timerSpawnRate = timeBetweenSpawns;
    }

    private void Update()
    {
        if (spawnGateActive)
        {
            timerSpawnRate += Time.deltaTime;

            if(timerSpawnRate >= timeBetweenSpawns && currentEnemies < maxEnemies)
            {
                SpawnEnemy();
                timerSpawnRate = 0;
            }
        }
    }

    private void SpawnEnemy()
    {
        currentEnemies++;
        GameObject enemy = Instantiate(enemyPrefabs[0], spawnLocation.position, spawnLocation.rotation);
        enemy.GetComponent<EnemyHealth>().SetSpawner(this);
        
    }

    public bool GetSpawnGateActive()
    {
        return spawnGateActive;
    }

    public void SetSpawnGateActive(bool currentState)
    {
        spawnGateActive = currentState;
    }

    public void EnemyDeath()
    {
        currentEnemies--;
    }
}

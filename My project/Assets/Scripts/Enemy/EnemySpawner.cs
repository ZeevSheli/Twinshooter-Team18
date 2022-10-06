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

    public AudioSource audioSource;
    public AudioClip enemySpawningSound;
    public AudioClip enemyDied;

    private void Start()
    {
        timerSpawnRate = timeBetweenSpawns;
    }

    private void Update()
    {
        if (spawnGateActive)
        {
            if(currentEnemies < maxEnemies)
            {
                timerSpawnRate += Time.deltaTime;
            }
            
            if(timerSpawnRate >= timeBetweenSpawns)
            {
                SpawnEnemy();
                timerSpawnRate = 0;
            }
        }
    }

    private void SpawnEnemy()
    {
        audioSource.PlayOneShot(enemySpawningSound, 0.83f);
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
        audioSource.PlayOneShot(enemyDied, 0.8f);
        currentEnemies--;
    }
}

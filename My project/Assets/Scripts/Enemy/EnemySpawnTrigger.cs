using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    EnemySpawner spawnGate;
    AudioSource enemyMusic;

    private void Start()
    {
        spawnGate = GetComponentInParent<EnemySpawner>();
        enemyMusic = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !spawnGate.GetSpawnGateActive())
        {
            spawnGate.SetSpawnGateActive(true);
            enemyMusic.Play();
        }

    }
}

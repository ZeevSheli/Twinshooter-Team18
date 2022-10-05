using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    EnemySpawner spawnGate;


    private void Start()
    {
        spawnGate = GetComponentInParent<EnemySpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !spawnGate.GetSpawnGateActive())
        {
            spawnGate.SetSpawnGateActive(true);
        }

    }
}

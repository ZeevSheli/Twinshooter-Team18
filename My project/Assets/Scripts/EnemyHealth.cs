using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemySpawner owner;
    [SerializeField] private int maxHealth= 3;
    private int currentHealth = 1;


    private void Update()
    {

        if(currentHealth <= 0)
        {
            Death();
        }   
        
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        owner = enemySpawner;
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        owner.EnemyDeath();
        Destroy(gameObject);
    }
}

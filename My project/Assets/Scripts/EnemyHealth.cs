using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemySpawner owner;
    [SerializeField] private int maxHealth= 3;
    private int currentHealth = 1;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public float volume;

    private void Update()
    {

        if(currentHealth <= 0)
        {
            audioSource.Play();
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

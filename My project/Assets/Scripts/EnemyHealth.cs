using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemySpawner owner;
    [SerializeField] private int maxHealth = 3;
    private int currentHealth = 1;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public GameObject firstHeart;
    public GameObject secondHeart;
    public GameObject thirdHeart;
    public GameObject fourthHeart;

    public float volume;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        DisplayHealth(); 

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
        audioSource.PlayOneShot(hurtSound, volume);
        currentHealth -= damage;
    }

    private void DisplayHealth()
    {

        if (currentHealth == 3)
        {
            fourthHeart.SetActive(false);
        }

        else if (currentHealth == 2)
        {
            thirdHeart.SetActive(false);
            fourthHeart.SetActive(false);
        }

        else if (currentHealth == 1)
        {
            secondHeart.SetActive(false);
            thirdHeart.SetActive(false);
            fourthHeart.SetActive(false);
        }

        else if (currentHealth == 0)
        {
            firstHeart.SetActive(false);
            secondHeart.SetActive(false);
            thirdHeart.SetActive(false);
            fourthHeart.SetActive(false);
        }
    }

    private void Death()
    { 
        owner.EnemyDeath();
        Destroy(gameObject);
    }
}

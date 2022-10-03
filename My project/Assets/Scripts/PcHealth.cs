using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public GameObject firstHeart;
    public GameObject middleHeart;
    public GameObject finalHeart;
    public GameObject deathUI;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        firstHeart.SetActive(true);
        middleHeart.SetActive(true);
        finalHeart.SetActive(true);
        deathUI.SetActive(false);

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet" && currentHealth >= 2)
        {
            audioSource.PlayOneShot(hurtSound, 0.8f);
            currentHealth--;
            Debug.Log("Shot but live");
        }
        else if (collider.tag == "Bullet" && currentHealth <= 1)
        {
            audioSource.PlayOneShot(hurtSound, 0.8f);
            currentHealth--;
            Death();
        }
       
        DisplayHealth();
    }


    void DisplayHealth()
    {
       
        if (currentHealth == 3)
        {
            firstHeart.SetActive(true);
            middleHeart.SetActive(true);
            finalHeart.SetActive(true);
        }
        
        else if (currentHealth == 2)
        {
            firstHeart.SetActive(false);
            middleHeart.SetActive(true);
            finalHeart.SetActive(true);
        }
        
        else if (currentHealth == 1)
        {
            firstHeart.SetActive(false);
            middleHeart.SetActive(false);
            finalHeart.SetActive(true);
        }

        else if (currentHealth == 0)
        {
            firstHeart.SetActive(false);
            middleHeart.SetActive(false);
            finalHeart.SetActive(false);
        }
    }
    void Death()
    {
        audioSource.PlayOneShot(deathSound, 0.6f);
        deathUI.SetActive(true);
        Debug.Log("OOF");
        
    }
}


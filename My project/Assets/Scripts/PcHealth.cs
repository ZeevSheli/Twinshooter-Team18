using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcHealth : MonoBehaviour
{
    public GameObject firstHeart;
    public GameObject middleHeart;
    public GameObject finalHeart;
    public GameObject deathUI;
    public int maxHealth = 3;
    public int currentHealth;   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        firstHeart.SetActive(true);
        middleHeart.SetActive(true);
        finalHeart.SetActive(true);
        deathUI.SetActive(false);
        // the player has 3 hearts 
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet" && currentHealth >= 2)
        {
            currentHealth--;
            Debug.Log("Shot but live");
        }
        else if (collider.tag == "Bullet" && currentHealth <= 0)
        {
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
        deathUI.SetActive(true);
        Debug.Log("OOF");
    }
}


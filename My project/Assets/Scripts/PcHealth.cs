using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcHealth : MonoBehaviour
{
    public GameObject firstHeart;
    public GameObject middleHeart;
    public GameObject finalHeart;
    public int maxHealth = 3;
    public int currentHealth;
    public bool collision;
   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        firstHeart.SetActive(true);
        middleHeart.SetActive(true);
        finalHeart.SetActive(true);

        // the player has 3 hearts 
    }

    // Update is called once per frame
    void TakingDamage()
    {
        if (collision = true && currentHealth >= 2)
        {
            currentHealth--;

        }
        else if (collision = true && currentHealth <= 0 || currentHealth == 1)
        {
            Death();
        }
                
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            collision = true;
        }
  
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

    }
    void Death()
    {
        Debug.Log("OOF");
    }
}


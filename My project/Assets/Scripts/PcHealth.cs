using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcHealth : MonoBehaviour
{
    public SpriteRenderer firstHeart;
    public SpriteRenderer middleHeart;
    public SpriteRenderer finalHeart;
    public int maxHealth = 3;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
            
            // the player has 3 hearts 
    }

    // Update is called once per frame
    void TakingDamage()
    {
        if (target.tag == "Bullet" && currentHealth >= 2)
        {
            currentHealth == currentHealth--;

        }
        else if (target.tag == "Bullet" && currentHealth =< 0 || currentHealth == 1)
        {
            Death();
        }
                
    }

    void DisplayHealth()
    {
       
        if (currentHealth == 3)
        {
            
        } 
      
        
           


            
    }
        void Death()
                {

        }
    }

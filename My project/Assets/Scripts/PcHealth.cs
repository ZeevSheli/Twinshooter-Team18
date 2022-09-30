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
        // if they are hit 3 times, they die and end condition is triggered
        // this needs to be saved, and not destroyed when function ends
    }

    void OnTriggerEnter()
    {
       // if (target.tag == "Bullet" && currentHealth => 2);
        {
      //      currentHealth = currentHealth - 1;
        }

    }
}

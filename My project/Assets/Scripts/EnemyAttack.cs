using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int enemyDamage = 1;
    
    
    // Start is called before the first frame update
    void Start()
    
    {
        print(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "Spear")
        {
            currentHealth -= enemyDamage;
            print("Spear just touched me!! OH NOES!" + currentHealth);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int enemyDamage = 1;
    [SerializeField] GameObject spear;
    
    // Start is called before the first frame update
    void Start()
    
    {
        //print(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        spear.SetActive(true);
    }

    public void HideWeapon()
    {
        spear.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) //OnCollisionEnter 
    {
        if (other.gameObject.tag == "Player")
        {
            //_collision.GetComponent<PcHealth>().ApplyDamage;
            Debug.Log("SPIKED");
            //currentHealth -= enemyDamage;
            //print("Spear just touched me!! OH NOES!" + currentHealth);
        }
    }
}

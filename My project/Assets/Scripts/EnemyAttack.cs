using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject spear;
    [SerializeField] private Animator anim; 
    public void Attack()
    {
        anim.SetTrigger("attack");
    }

}

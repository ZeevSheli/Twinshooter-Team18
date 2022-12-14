using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float attackRange = 2f;
    Transform target;
    NavMeshAgent agent;
    
    EnemyAttack enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;    
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chasing the player if they are within radius
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= attackRange)
            {
                // Attack Code here

                // Call Face the target function
                enemyAttack.Attack();
                FaceTarget();
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Displays Enemy Detection range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}

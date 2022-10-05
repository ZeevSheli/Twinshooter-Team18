using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Ricochet Modifiers")]
    [Tooltip("The elements within this array represent the projectile scale based on the remaining bounces. Note that this means that element 0 represents the final projectile scale.")]
    [SerializeField] private float[] ricochetScale;
    [Tooltip("The elements within this array represent the projectile ricochet impulse based on the remaining bounces. Note that this means that element 0 represents the final impulse.")]
    [SerializeField] private float[] ricochetImpulse;
    private float currentImpulse;
    private Rigidbody rigidBody;
    private int ricochetCount = 0;
    private int currentBounce = -1;

    [Header("Aim Assist Attributes")]
    [Tooltip("The objects on these layers will pull the projectile based on the Aim Assist Radius.")]
    [SerializeField] private LayerMask aimAssistLayer;
    [Tooltip("SphereCast Radius sent along the actual ricochet direction to determine if any targets are in range on the Aim Assist Layer. A hit will cause the bullet to alter its direction towards the target.")]
    [SerializeField] private float aimAssistRadius;
    //Transform desiredTarget; //Could be used to improve aim assist
    //bool aimAssistActive;

    [SerializeField] EnemySpawner enemySpawnMEGAPLACEHOLDER;

    Vector3 directionToTarget;

    public AudioSource audioSource;
    public AudioClip bounceSound;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rigidBody.AddRelativeForce(transform.forward * currentImpulse, ForceMode.VelocityChange);
        enemySpawnMEGAPLACEHOLDER = GameObject.Find("EnemySpawnGate").GetComponent<EnemySpawner>(); //REMOOOOOOOVE
    }


    private void Update()
    {
        if(ricochetCount < 0)
        {
            Destroy(gameObject);
        }

        //Update AimBOT Target --- Get Target on ricochet with a sphere cast. Set EnemySet initial direction the same? but update gradually with acceleration + Clamp maxspeed.
        //Detect if target was either Enemy or ricochetSurface. If that is the case set target. if target was set we don't update target!! Only get target on Ricochet call!
    }

    public void SetImpulse(float projectileImpulse)
    {
        currentImpulse = projectileImpulse;
    }

    public void SetRiochetAmount(int amount)
    {
        ricochetCount = amount;
    }

    private void SetScale(int remainingBounces)
    {
        transform.localScale = new Vector3(ricochetScale[remainingBounces], ricochetScale[remainingBounces], ricochetScale[remainingBounces]);
    }

    private void RicochetProjectile(ContactPoint hitPoint)
    {
        audioSource.PlayOneShot(bounceSound, 0.6f);

        if (!hitPoint.otherCollider.CompareTag("RicochetTarget"))
        {
            
            ricochetCount--;
        }

        if(currentBounce < ricochetImpulse.Length - 1)
        {
            currentBounce++;
        }
              
        if(ricochetCount >= 0)
        {
            SetImpulse(ricochetImpulse[currentBounce]);
            Vector3 bounceDirection = Vector3.Reflect(transform.forward, hitPoint.normal);

            if (FindAimTarget(bounceDirection))
            {
                //Debug.Log("FOUND TARGET"); 
                //aimAssistActive = true; //extra stuff for dynamic aim assist mid movement.
                AimAssist();
            }
            else
            {
                //Debug.Log("NO TARGET FOUND");
                //desiredTarget = null; //does nothing currently. Comes into play when gradually curving towards moving target
                //aimAssistActive = false;
            }

            transform.forward = directionToTarget;
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(transform.forward * currentImpulse, ForceMode.VelocityChange); /// Then make bounce layer
            SetScale(currentBounce);           
        }
        
    }

    private bool FindAimTarget(Vector3 direction)
    {
        Debug.DrawRay(transform.position, direction * 5f, Color.green, 2f);
        
        if(Physics.SphereCast(transform.position, aimAssistRadius, direction, out RaycastHit impact, Mathf.Infinity, aimAssistLayer)) //IMPROVE WITH SPHERECASTALL??? Prioritize enemies, ricochet and then wall
        {        
            directionToTarget = impact.transform.position - transform.position;
            return true;                
        }
        
        directionToTarget = direction;       
        return false;
    }

    private void AimAssist()
    {
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        ContactPoint hit = collision.GetContact(0);
        RicochetProjectile(hit);

        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            enemySpawnMEGAPLACEHOLDER.EnemyDeath(); //THIIIIIS NEEEEEDS TOOOO BE MOOOOVED ---- Do it when I get access to Enemy script ///Make enemies move to random position at spawn. Enemy Nav Obstacle
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Void"))
        {
            Destroy(gameObject);
        }

    }

}

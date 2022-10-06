using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Ricochet Modifiers")]
    [Tooltip("The elements within this array represent the projectile damage based on the current bounce count. Note that 0 represents the first bounce")]
    [SerializeField] private int[] ricochetDamage;
    [Tooltip("The elements within this array represent the projectile scale based on the current bounce count. Note that 0 represents the first bounce")]
    [SerializeField] private float[] ricochetScale;
    [Tooltip("The elements within this array represent the projectile impulse based on the current bounce count. Note that 0 represents the first bounce")]
    [SerializeField] private float[] ricochetImpulse;
    private float currentImpulse;
    private Rigidbody rigidBody;
    private int ricochetCount = 0;
    private int currentBounce = -1;

    private int damage = 0;

    [Header("Aim Assist Attributes")]
    [Tooltip("The objects on these layers will pull the projectile based on the Aim Assist Radius.")]
    [SerializeField] private LayerMask aimAssistLayer;
    [Tooltip("SphereCast Radius sent along the actual ricochet direction to determine if any targets are in range on the Aim Assist Layer. A hit will cause the bullet to alter its direction towards the target.")]
    [SerializeField] private float aimAssistRadius;

    [Header("Hit FX")]
    [SerializeField] private ParticleSystem hitRicochet;
    [SerializeField] private ParticleSystem hitShield;
    [SerializeField] private ParticleSystem hitEnemy;

    Vector3 directionToTarget;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip goodBounceSound;
    public AudioClip badBounceSound;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rigidBody.AddRelativeForce(transform.forward * currentImpulse, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if(ricochetCount < 0)
        {
            Destroy(gameObject);
        }

        switch (currentBounce)
        {
            case 0:
                damage = ricochetDamage[0];
                break;
            case 1:
                damage = ricochetDamage[1];
                break;
            case 2:
                damage = ricochetDamage[2];
                break;
        }
       
    }

    public void SetImpulse(float projectileImpulse)
    {
        currentImpulse = projectileImpulse;
    }

    public void SetRiochetAmount(int amount)
    {
        ricochetCount = amount;
    }

    public void SetDamage(int projectileDamage)
    {
        damage = projectileDamage;
    }

    private void SetScale(int remainingBounces)
    {
        transform.localScale = new Vector3(ricochetScale[remainingBounces], ricochetScale[remainingBounces], ricochetScale[remainingBounces]);
    }

    private void RicochetProjectile(ContactPoint hitPoint)
    {
        
        if (!hitPoint.otherCollider.CompareTag("RicochetTarget")) //Projectile ricochet on something not specified as RicochetTarget
        {
            audioSource.PlayOneShot(badBounceSound, 0.4f);
            ricochetCount--;
        }
        else //Projectile ricochet on RicochetTarget (Yellow Marker)
        {            
            audioSource.PlayOneShot(goodBounceSound, 0.6f);
            Instantiate(hitRicochet, hitPoint.point, transform.rotation);
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
                AimAssist();
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
        
        if(Physics.SphereCast(transform.position, aimAssistRadius, direction, out RaycastHit impact, Mathf.Infinity, aimAssistLayer))
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
            collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(damage);
            Instantiate(hitEnemy, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
        }

        Debug.Log(collision.gameObject.name);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Void"))
        {
            Destroy(gameObject);
            Instantiate(hitShield, other.transform.position, other.transform.rotation);
        }

    }

}

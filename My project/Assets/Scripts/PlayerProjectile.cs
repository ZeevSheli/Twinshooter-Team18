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
    private int ricochetCount = 1;
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
        ricochetCount--;
        SetImpulse(ricochetImpulse[ricochetCount]);
        Vector3 bounceDirection = Vector3.Reflect(transform.forward, hitPoint.normal);
        transform.forward = bounceDirection;
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.forward * currentImpulse, ForceMode.VelocityChange); /// Then make bounce layer
        SetScale(ricochetCount);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        ContactPoint hit = collision.GetContact(0);
        RicochetProjectile(hit);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float impulse;
    private Rigidbody rigidBody;
    private int ricochetCount = 1;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rigidBody.AddRelativeForce(transform.forward * impulse, ForceMode.VelocityChange);
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
        impulse = projectileImpulse;
    }

    public void SetRiochetAmount(int amount)
    {
        ricochetCount = amount;
    }

    private void RicochetProjectile(ContactPoint hitPoint)
    {
        Vector3 bounceDirection = Vector3.Reflect(transform.forward, hitPoint.normal);
        transform.forward = bounceDirection;
        //rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.forward * impulse, ForceMode.VelocityChange); ///MAKE SURE WE TRIGGER THIS EVERY TIME WE HIT SOMETHING!!! Then make bounce layer
    }

    private void OnCollisionEnter(Collision collision)
    {
        ricochetCount--;
        ContactPoint hit = collision.GetContact(0);
        RicochetProjectile(hit);

    }

}

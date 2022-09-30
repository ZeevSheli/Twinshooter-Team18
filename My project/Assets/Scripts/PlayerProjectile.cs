using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 defaultScale;
    [SerializeField] private float[] ricochetScale;
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
        defaultScale = transform.localScale;
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
        rigidBody.AddForce(transform.forward * currentImpulse, ForceMode.VelocityChange); /// Then make bounce layer
        SetScale(ricochetCount);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        ContactPoint hit = collision.GetContact(0);
        RicochetProjectile(hit);
    }

}

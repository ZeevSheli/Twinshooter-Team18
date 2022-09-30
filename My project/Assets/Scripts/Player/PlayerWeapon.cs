using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{

    [Header("Weapon Attributes")]
    [Tooltip("Determines the rate of fire for the weapon.")]
    [SerializeField] private float timeBetweenShots;
    private float timerFireRate;
    [Tooltip("The launch impulse that the projectile is recieved upon leaving the gunBarrel. Does not account for rigidbody mass. ")]
    [SerializeField] private float projectileImpulse;
    [Tooltip("Damage dealt by bullet when hitting a desirable target.")]
    [SerializeField] private float projectileDamage;
    [Tooltip("The amount of bounces a ricochet bullet is allowed to perform before being destroyed.")]
    [SerializeField] private int ricochetBounces;
    [Tooltip("Overrides ricochetBounces by destroying bullet after set amount of time. Can be used as a failsafe to avoid potential bullet stacking in the level.")]
    [SerializeField] private float projectileLifeTime;

    [Header("Required Components")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform gunBarrel;

    private void Start()
    {
        timerFireRate = timeBetweenShots;
    }

    void Update()
    {
        timerFireRate += Time.deltaTime;

        if (PlayerCombatController.Instance.FireInputPressed && timerFireRate >= timeBetweenShots)
        {
            FireProjectile();
        }
        else
        {
            PlayerCombatController.Instance.FireInputPressed = false;
        }
    }

    void FireProjectile()
    {
        GameObject cloneProjectile = Instantiate(projectile, gunBarrel.position, Quaternion.identity);
        cloneProjectile.transform.forward = gunBarrel.transform.forward;
        Destroy(cloneProjectile, projectileLifeTime);
        cloneProjectile.GetComponent<PlayerProjectile>().SetImpulse(projectileImpulse);
        cloneProjectile.GetComponent<PlayerProjectile>().SetRiochetAmount(ricochetBounces);

        PlayerCombatController.Instance.FireInputPressed = false;
        timerFireRate = 0f;
    }


}

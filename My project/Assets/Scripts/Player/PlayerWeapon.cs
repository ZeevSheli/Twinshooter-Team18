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
    [Tooltip("The launch impulse that the projectile recieves upon leaving the Gun Barrel. Does not account for rigidbody mass.")]
    [SerializeField] private float projectileImpulse;
    [Tooltip("Damage dealt by bullet when hitting a desirable target.")]
    [SerializeField] private float projectileDamage;
    [Tooltip("The amount of bounces a ricochet bullet is allowed to perform before being destroyed. Note that this needs to correspond with the Ricochet Modifer sizes set on the projectile Prefab")]
    [SerializeField] private int ricochetBounces;
    [Tooltip("Overrides Ricochet Bounces by destroying bullet after set amount of time. Can be used as a failsafe to avoid potential bullet stacking in the level.")]
    [SerializeField] private float projectileLifeTime;

    [Header("Required Components")]
    [Tooltip("The projectile that the weapon should spawn.")]
    [SerializeField] private GameObject projectile;
    [Tooltip("Spawn point for instantiated projectile.")]
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

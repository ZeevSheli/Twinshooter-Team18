using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [Header("Shield Audio")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip shieldImpact;
    [SerializeField] private float audioVolume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            audioSource.PlayOneShot(shieldImpact, audioVolume);
        }
    }
}

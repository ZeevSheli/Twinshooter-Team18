using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserSight : MonoBehaviour
{
    private PlayerMovementController playerMovementController;
    private LineRenderer laserSight;

    private void Start()
    {
        playerMovementController = GetComponentInParent<PlayerMovementController>();
        laserSight = GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {
        if(playerMovementController.GetLookRotation() != Vector2.zero)
        {
            laserSight.enabled = true;
        }
        else
        {
            laserSight.enabled = false;
        }
    }
}

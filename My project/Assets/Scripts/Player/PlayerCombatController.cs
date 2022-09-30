using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{

    public static PlayerCombatController Instance;

    [HideInInspector]
    public bool FireInputPressed = false;

    private void Awake()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    void OnFire(InputValue fireInput)
    {
 
        if (fireInput.isPressed)
        {
            FireInputPressed = true;
        }

    }
}

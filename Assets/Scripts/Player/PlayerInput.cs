using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [HideInInspector]
    public PlayerActions input;

    private void OnEnable()
    {
        // Enable the input actions when the object is enabled
        input.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the object is disabled
        input.Disable();
    }

    private void Awake() 
    {
        input = new PlayerActions();

        input.Keyboard.Move.performed += ctx => Move(ctx);
        input.Keyboard.Move.canceled += ctx => Move(ctx);
        input.Keyboard.Jump.performed += ctx => JumpPressed(ctx);
        input.Keyboard.Grapple.performed += ctx => GrapplePressed(ctx);
        input.Keyboard.Boost.performed += ctx => Boost(ctx);
    }

    private void Boost(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Debug.Log("Boost Cancelled");
            return;
        }

        Debug.Log("Boost started");
    }

    private void GrapplePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Debug.Log("Grapple Released");
            return;
        }
        Debug.Log("Grapple started");
    }

    private void JumpPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Debug.Log("Jump Cancelled");
            return;
        }
        Debug.Log("Jump started");
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Debug.Log("Movement Cancelled"); 
            return;
        }
        Debug.Log("Movement started");
    }

    void Update()
    {
        
    }
}

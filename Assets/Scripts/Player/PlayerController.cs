using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public PlayerActions playerInput;

    private void OnEnable()
    {
        // Enable the input actions when the object is enabled
        playerInput.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the object is disabled
        playerInput.Disable();
    }

    private void Awake() 
    {
        playerInput = new PlayerActions();

        playerInput.Keyboard.Move.performed += ctx => Move(ctx);
        playerInput.Keyboard.Move.canceled += ctx => Move(ctx);
        playerInput.Keyboard.Jump.performed += ctx => JumpPressed(ctx);
        playerInput.Keyboard.Grapple.performed += ctx => GrapplePressed(ctx);
        playerInput.Keyboard.Boost.performed += ctx => Boost(ctx);
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

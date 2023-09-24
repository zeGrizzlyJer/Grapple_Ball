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
        input.Keyboard.PrimaryAction.performed += ctx => PrimaryActionPressed(ctx);
        input.Keyboard.SecondaryAction.performed += ctx => SecondaryActionPressed(ctx);
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
        FindObjectOfType<AudioManager>().Play("Boost");
    }

    private void PrimaryActionPressed(InputAction.CallbackContext ctx)
    {
        /*if (ctx.canceled)
        {
            Debug.Log("Grapple Released");
            return;
        }*/
        Debug.Log("Ability deployed");
    }

    private void SecondaryActionPressed(InputAction.CallbackContext ctx)
    {
        /*if (ctx.canceled)
        {
            Debug.Log("Grapple Released");
            return;
        }*/
        Debug.Log("Ability released");
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

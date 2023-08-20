using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    [SerializeField] private PlayerAbility ability;

    public float gravity;
    public float acceleration;
    public float maxSpeed;

    private Vector2 inputDirection;
    private Vector3 playerDirection;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        if (!ability) Debug.Log("Please set an ability on: " + gameObject.name);

        playerInput.input.Keyboard.Move.performed += ctx => Move(ctx);
        playerInput.input.Keyboard.Move.canceled += ctx => Move(ctx);

        if (ability)
        {
            playerInput.input.Keyboard.PrimaryAction.performed += ctx => PrimaryAction(ctx);
            playerInput.input.Keyboard.SecondaryAction.performed += ctx => SecondaryAction(ctx);
        }
    }

    private void Update()
    {
        Vector3 forwardXZ = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        playerDirection = ((forwardXZ * inputDirection.y) + (transform.right * inputDirection.x)) * acceleration - Vector3.up * gravity;

        rb.AddForce(playerDirection);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            inputDirection = Vector2.zero;
            Debug.Log("Stopped");
            return;
        }
        inputDirection = ctx.action.ReadValue<Vector2>();
        Debug.Log("Movin");
    }

    private void PrimaryAction(InputAction.CallbackContext ctx)
    {
        Debug.Log(gameObject.name + " is calling primary ability");
        ability.PrimaryUse();
    }

    private void SecondaryAction(InputAction.CallbackContext ctx)
    {
        Debug.Log(gameObject.name + " is calling secondary ability");
        ability.SecondaryUse();
    }
}

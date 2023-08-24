using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;
using GrappleBall;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rBody;
    [SerializeField] private PlayerAbility ability;

    public float gravity;
    public float acceleration;
    public float maxSpeed;

    private Vector2 inputDirection;
    private Vector3 playerDirection;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody>();
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
        Movement();
        AbilityBehaviour();
    }

    private void Movement()
    {
        Vector3 forwardXZ = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        playerDirection = ((forwardXZ * inputDirection.y) + (transform.right * inputDirection.x)) * acceleration - Vector3.up * gravity;

        rBody.AddForce(playerDirection);
        if (rBody.velocity.magnitude > maxSpeed)
        {
            rBody.velocity = rBody.velocity.normalized * maxSpeed;
        }

        /*Vector3 accel = ((inputDirection.y * forwardXZ) + (inputDirection.x * transform.right)) * acceleration + (Vector3.down * gravity);
        rBody.velocity += accel * Time.deltaTime;
        if (rBody.velocity.magnitude > maxSpeed)
        {
            rBody.velocity = rBody.velocity.normalized * maxSpeed;
        }*/
    }

    private void AbilityBehaviour()
    {
        if (ability.Ability == PlayerAbilities.Grapple)
        {
            if (!ability.IsGrappled()) return;
            Vector3 grapplePosition = ability.transform.position;
            float distance = (grapplePosition - transform.position).magnitude;

            if (distance >= ability.AbilityLength())
            {
                Vector3 normal = (grapplePosition - transform.position).normalized;
                transform.position = grapplePosition - ability.AbilityLength() * normal;

                float dotProduct = Vector3.Dot(rBody.velocity, normal);
                Vector3 projection = rBody.velocity - dotProduct * normal;

                rBody.velocity = projection;
            }
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
        Debug.Log("Moving");
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

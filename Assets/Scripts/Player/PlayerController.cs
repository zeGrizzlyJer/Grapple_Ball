using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;
using GrappleBall;
using System.Data;
using Input = UnityEngine.Input;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private PlayerInput pInput;
    private Rigidbody rBody;
    private Boost playerBoost;

    [SerializeField] private PlayerAbility ability;

    public Transform playerInputSpace = default;
    [SerializeField, Range(0f, 100f)] private float maxSpeed;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f, maxAirAcceleration = 1f;
    [SerializeField, Range(1f, 10f)] private float jumpHeight = 2f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 90f)] private float maxGroundAngle = 25f, maxStairsAngle = 50f;
    [SerializeField, Range(0f, 100f)] private float maxSnapSpeed = 100f;
    [SerializeField, Min(0f)] private float probeDistance = 1f;
    [SerializeField] LayerMask probeMask = -1, stairsMask = -1;
    private Vector3 upAxis, rightAxis, forwardAxis;
    private bool desiredJump;
    private int groundContactCount, steepContactCount;
    private int stepsSinceLastGrounded, stepsSinceLastJump;
    private bool OnGround => groundContactCount > 0;
    private bool OnSteep => steepContactCount > 0;
    private int jumpPhase;
    private Vector3 velocity, desiredVelocity;
    private Vector3 contactNormal, steepNormal;
    private float minGroundDotProduct, minStairsDotProduct;

    private bool jumpAttempt;
    private Vector2 playerInput;
    //private Vector3 playerDirection;

    public float GetSpeed => velocity.magnitude;
    public float GetMaxSpeed => maxSpeed;

    private void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
    }

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        pInput = GetComponent<PlayerInput>();
        playerBoost = GetComponent<Boost>();
        rBody.useGravity = false;

        if (!ability) Debug.Log("Please set an ability on: " + gameObject.name);
        OnValidate();
    }

    private void Start()
    {
        pInput.input.Keyboard.Move.performed += ctx => Move(ctx);
        pInput.input.Keyboard.Move.canceled += ctx => Move(ctx);
        pInput.input.Keyboard.Jump.started += ctx => JumpCall(ctx);
        pInput.input.Keyboard.Jump.canceled += ctx => JumpCall(ctx);

        if (ability)
        {
            pInput.input.Keyboard.PrimaryAction.performed += ctx => PrimaryAction(ctx);
            pInput.input.Keyboard.SecondaryAction.performed += ctx => SecondaryAction(ctx);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Movement();
        AbilityBehaviour();
    }

    private void FixedUpdate()
    {
        Vector3 gravity = CustomGravity.GetGravity(rBody.position, out upAxis);
        UpdateState();
        AdjustVelocity();

        if (desiredJump)
        {
            desiredJump = false;
            jumpAttempt = false;
            Jump(gravity);
        }

        velocity += gravity * Time.deltaTime;

        rBody.velocity = velocity;
        ClearState();
    }

    private void ClearState()
    {
        groundContactCount = steepContactCount = 0;
        contactNormal = steepNormal = Vector3.zero;
    }

    private void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = rBody.velocity;
        if (OnGround || SnapToGround() || CheckSteepContacts())
        {
            stepsSinceLastGrounded = 0;
            if (stepsSinceLastJump > 1) jumpPhase = 0;
            if (groundContactCount > 1) contactNormal.Normalize();
        }
        else contactNormal = upAxis;
    }

    private void Jump(Vector3 gravity)
    {
        Vector3 jumpDirection;

        if (OnGround)
        {
            jumpDirection = contactNormal;
        }
        else if (OnSteep)
        {
            jumpDirection = steepNormal;
            jumpPhase = 0;
        }
        else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0) jumpPhase = 1;
            jumpDirection = contactNormal;
        }
        else return;

        stepsSinceLastJump = 0;
        jumpPhase += 1;
        float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * jumpHeight);
        jumpDirection = (jumpDirection + upAxis).normalized;
        float alignedSpeed = Vector3.Dot(velocity, jumpDirection);
        if (alignedSpeed > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }
        velocity += jumpDirection * jumpSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        float minDot = GetMinDot(collision.gameObject.layer);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            float upDot = Vector3.Dot(upAxis, normal);
            if (upDot >= minDot)
            {
                groundContactCount += 1;
                contactNormal += normal;
            }
            else if (upDot > -0.01f)
            {
                steepContactCount += 1;
                steepNormal += normal;
            }
        }
    }

    private Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
    {
        return (direction - normal * Vector3.Dot(direction, normal)).normalized;
    }

    private void AdjustVelocity()
    {
        Vector3 xAxis = ProjectDirectionOnPlane(rightAxis, contactNormal);
        Vector3 zAxis = ProjectDirectionOnPlane(forwardAxis, contactNormal);

        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);

        float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;

        float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    private bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
        {
            return false;
        }
        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed)
        {
            return false;
        }
        if (!Physics.Raycast(rBody.position, -upAxis, out RaycastHit hit, probeDistance, probeMask))
        {
            return false;
        }
        float upDot = Vector3.Dot(upAxis, hit.normal);
        if (upDot < GetMinDot(hit.collider.gameObject.layer))
        {
            return false;
        }

        groundContactCount = 1;
        contactNormal = hit.normal;
        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f)
        {
            velocity = (velocity - hit.normal * dot).normalized * speed;
        }
        return true;
    }

    private float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }

    private bool CheckSteepContacts()
    {
        if (steepContactCount > 1)
        {
            steepNormal.Normalize();
            float upDot = Vector3.Dot(upAxis, steepNormal);
            if (upDot >= minGroundDotProduct)
            {
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }
        }
        return false;
    }

    private void Movement()
    {
        if (playerInputSpace)
        {
            rightAxis = ProjectDirectionOnPlane(playerInputSpace.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(playerInputSpace.forward, upAxis);
        }
        else
        {
            rightAxis = ProjectDirectionOnPlane(Vector3.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(Vector3.forward, upAxis);
        }
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        desiredJump |= jumpAttempt;


        /*Vector3 forwardXZ = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        playerDirection = ((forwardXZ * playerInput.y) + (transform.right * playerInput.x)) * acceleration - Vector3.up * gravity;

        rBody.AddForce(playerDirection);
        if ((rBody.velocity.magnitude > maxSpeed) && !playerBoost.isBoosting) 
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
            playerInput = Vector2.zero;
            Debug.Log("Stopped");
            return;
        }
        playerInput = ctx.action.ReadValue<Vector2>();
        Debug.Log("Moving");
    }

    private void JumpCall(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            jumpAttempt = false;
            Debug.Log("Jump Reset");
            return;
        }
        jumpAttempt = true;
        Debug.Log("Attempt Jump");
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

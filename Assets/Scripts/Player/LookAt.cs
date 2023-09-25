using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class LookAt : MonoBehaviour
{
    [Flags]
    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    [SerializeField] private Transform playerInputSpace;

    [SerializeField] private RotationDirection rotationDirection;
    [SerializeField] private Vector2 acceleration;
    [SerializeField] private Vector2 sensitivity;
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    private void OnEnable()
    {
        velocity = Vector2.zero;
        inputLagTimer = 0;
        lastInputEvent = Vector2.zero;

        Vector3 euler = transform.localEulerAngles;

        if (euler.x >= 180) euler.x -= 360;

        euler.x = ClampVerticalAngle(euler.x);
        transform.localEulerAngles = euler;

        rotation = new Vector2(euler.y, euler.x);
    }

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngle, maxVerticalAngle);
    }

    private Vector2 GetInput()
    {
        inputLagTimer += Time.deltaTime;
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

        if (!(Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }

    private void Update()
    {
        Vector2 desiredVelocity = GetInput() * sensitivity;

        if ((rotationDirection & RotationDirection.Horizontal) == 0) desiredVelocity.x = 0;
        if ((rotationDirection & RotationDirection.Vertical) == 0) desiredVelocity.y = 0;

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, desiredVelocity.y, acceleration.y * Time.deltaTime));

        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);

        if (Input.GetKeyDown("1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKey("2"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
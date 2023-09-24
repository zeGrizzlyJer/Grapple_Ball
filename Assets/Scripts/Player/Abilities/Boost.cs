using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boost : MonoBehaviour
{
    [SerializeField] private float boostDuration = 2f;
    [SerializeField] private float boostSpeedMultiplier = 2f; // Adjust this value for the desired boost strength

    private PlayerInput playerInput;
    private Rigidbody rBody;
    public bool isBoosting = false; 
    private Vector3 originalVelocity; // Store the original velocity when boosting starts

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody>(); 

        playerInput.input.Keyboard.Boost.performed += ctx => StartBoost(ctx); 
    }

    private void StartBoost(InputAction.CallbackContext ctx)
    {
        if (!isBoosting)
        {
            isBoosting = true;
            originalVelocity = rBody.velocity; // Store the original velocity
            Debug.Log("BOOSTED");
            FindObjectOfType<AudioManager>().Play("Boost");

            // Apply the boost by multiplying the current velocity
            rBody.velocity *= boostSpeedMultiplier;

            // Scheduling boost to end after boostDuration seconds
            StartCoroutine(EndBoost());
        }
    }

    private IEnumerator EndBoost()
    {
        yield return new WaitForSeconds(boostDuration);

        // Revert the velocity to its original value
        rBody.velocity = originalVelocity;
        Debug.Log("ENDED BOOST");
        isBoosting = false;
    }
}

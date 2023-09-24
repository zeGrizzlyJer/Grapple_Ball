using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Coin : Interactables
{
    public float rotationSpeed;
    public float oscillateSpeed;
    public float oscillateHeight;

    private float startHeight;

    [SerializeField] private GameObject burstPrefab;
    [SerializeField] private GameObject bubblePrefab; 

    protected virtual void Start() 
    {
        startHeight = transform.position.y; 
    }

    protected virtual void Update() 
    {
        Vector3 euler = transform.localEulerAngles;
        if (euler.y > 360) euler.y -= 360;
        euler.y += rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = euler;

        float newHeight = startHeight + oscillateHeight * Mathf.Sin(oscillateSpeed * Time.time);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    protected override void OnTriggerEnter(Collider other)   
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(burstPrefab, transform.position, transform.rotation);
            Instantiate(bubblePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
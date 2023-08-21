using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickUpCoin : MonoBehaviour
{
    public int points = 0;
    public float rotationSpeed;
    public float oscillateSpeed;
    public float oscillateHeight;

    private float startHeight;

    private void Start()
    {
        startHeight = transform.position.y;
    }

    private void Update()
    {
        Vector3 euler = transform.localEulerAngles;
        if (euler.y > 360) euler.y -= 360;
        euler.y += rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = euler;

        float newHeight = startHeight + oscillateHeight * Mathf.Sin(oscillateSpeed * Time.time);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Score added: " + points);
            Destroy(gameObject);
        }
    }
}
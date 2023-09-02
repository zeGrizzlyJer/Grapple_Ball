using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimerCoin : MonoBehaviour
{
    public int points = 0;
    public float rotationSpeed;
    public float oscillateSpeed;
    public float oscillateHeight;

    private float startHeight;

    //I added
    public GameObject timerObject; 
    private TimerManager timer; 

    private void Start()
    {
        startHeight = transform.position.y;

        //I added below
        timerObject = GameObject.FindGameObjectWithTag("TimerObject");
        if (timerObject != null)
        {
            timer = timerObject.GetComponent<TimerManager>(); timerObject = GameObject.FindGameObjectWithTag("TimerObject");
        }
        if (timerObject != null)
        {
            timer = timerObject.GetComponent<TimerManager>();
        }
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

            if (timer.isRunning)
            {
                timer.coinCount++; //I added
            }
            Debug.Log("Score added: " + points);
            Destroy(gameObject);
        }
    }
}
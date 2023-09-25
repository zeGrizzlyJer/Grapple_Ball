using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab; // The prefab of the coin to be collected
    [SerializeField] private GameObject prizePrefab; // The prefab of the powerup that appears when all coins are collected
    [SerializeField] private Transform timerStarter;
    [SerializeField] private TimerCollider timerColliderScript;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform winPoint;

    [SerializeField] private int coinAmount = 3; // How many coins player has to collect before the timer runs out
    [SerializeField] private float timerEnd = 10f; // 10 seconds 

    public int coinCount = 0; // Coins collected 

    private float startTime;
    private float stopTime;
    private float timerTime;
    public bool isRunning = false;

    private GameObject[] instantiatedPickups;

    // Properties for minutes, seconds, and milliseconds
    public int Minutes
    {
        get => (int)timerTime / 60;
        set => timerTime = value * 60 + Seconds + Milliseconds / 100.0f;
    }

    public int Seconds
    {
        get => (int)timerTime % 60;
        set => timerTime = Minutes * 60 + value + Milliseconds / 100.0f;
    }

    public int Milliseconds
    {
        get => (int)(Mathf.Floor((timerTime - (Seconds + Minutes * 60)) * 100));
        set => timerTime = Minutes * 60 + Seconds + value / 100.0f;
    }

    void Start()
    {
        instantiatedPickups = new GameObject[coinAmount]; // Initialize the array
        TimerReset();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            isRunning = true;
            startTime = Time.time;

            // Instantiating coins to be collected
            for (int i = 0; i < coinAmount; i++)
            {
                instantiatedPickups[i] = Instantiate(pickupPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            }
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            isRunning = false;
            stopTime = timerTime;

            // Destroys all the pickups instantiated
            for (int i = 0; i < coinAmount; i++)
            {
                Destroy(instantiatedPickups[i]);
            }

            if (coinCount >= coinAmount)
            {
                Instantiate(prizePrefab, winPoint.position, winPoint.rotation); // Instantiate the prize
            }
            else
            {
                timerColliderScript.timerActive = true;  
                TimerReset(); 
            } 
        }
    }

    public void TimerReset()
    {
        stopTime = 0;
        isRunning = false;
        coinCount = 0;
    }

    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);

        if (isRunning)
        {
            Debug.LogFormat("Time: {0:00}:{1:00}:{2:00}", Minutes, Seconds, Milliseconds);
        }

        // Check if the timer has reached 10 seconds
        if (timerTime >= timerEnd)
        {
            TimerStop();
        }

        if (coinCount == coinAmount)
        {
            TimerStop();
        }

        StatHolder.timer = timerTime;
        StatHolder.score = coinCount;
    }
}

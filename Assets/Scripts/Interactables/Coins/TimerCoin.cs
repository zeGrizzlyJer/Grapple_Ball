using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimerCoin : Coin 
{
    private GameObject timerObject;  
    private TimerManager timer;

    protected override void Start() 
    {
        base.Start(); 

        timerObject = GameObject.FindGameObjectWithTag("TimerObject");
        if (timerObject != null) timer = timerObject.GetComponent<TimerManager>(); timerObject = GameObject.FindGameObjectWithTag("TimerObject");
        if (timerObject != null) timer = timerObject.GetComponent<TimerManager>();
    }

    protected override void Update() 
    {
        base.Update(); 
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            if (timer.isRunning)
            {
                timer.coinCount++; 
            }
            Debug.Log("Timer Coins Collected: " + timer.coinCount); 
        }
        base.OnTriggerEnter(other); 
    }
}
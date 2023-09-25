using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCollider : Interactables 
{
    BoxCollider boxCollider;
    private TimerManager timer;
    public bool timerActive; 

    private void Start()
    {
        timer = GameObject.FindGameObjectWithTag("TimerObject").GetComponent<TimerManager>();   
        boxCollider = GetComponent<BoxCollider>();
        timerActive = true; 
        if (!timer) Debug.Log($"Timer not found on {name}");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   
        {
            Debug.Log("Timer Started"); 
            timer.TimerStart();
            timerActive = false;

            GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>().enabled = true;
        }
    }

    public void Update()
    {
        boxCollider.enabled = timerActive; 
    }
}

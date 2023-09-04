using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCollider : Interactables 
{
    [SerializeField] private TimerManager timer;

    private void Start()
    {
        if (!timer) Debug.Log($"Timer not found on {name}");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   
        {
            Debug.Log("Timer Started"); 
            timer.TimerStart();
            Destroy(gameObject); 
        }
    } 
}

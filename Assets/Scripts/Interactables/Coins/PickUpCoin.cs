using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickUpCoin : Coin
{
    public int points = 0; 

     protected override void Start()  
    {
        base.Start(); 
    }

    protected override void Update()  
    {
        base.Update(); 
    }

    protected override void OnTriggerEnter(Collider other)  
    {
        if (other.CompareTag("Player")) Debug.Log("Score added: " + points);
        base.OnTriggerEnter(other);
    }
}
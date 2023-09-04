using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)  
    {
        Debug.Log( other.name + "Collision Detected.");
    }
} 

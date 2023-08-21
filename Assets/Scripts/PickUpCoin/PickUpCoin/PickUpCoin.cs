using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickUpCoin : MonoBehaviour
{
    public int points = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            points++;
            Debug.Log("ScoreAdded" + points);
        }
    }
}




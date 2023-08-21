using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] pickups;
    public Transform[] spawnpoints;

    void Start()
    {
        int pickupsize = pickups.Length;
        for (int i = 0; i < spawnpoints.Length; i++)
        {

            int CoinPickup = Random.Range(0, pickupsize);
            Instantiate(pickups[CoinPickup], spawnpoints[i].position, spawnpoints[i].rotation);
        }
    }
}
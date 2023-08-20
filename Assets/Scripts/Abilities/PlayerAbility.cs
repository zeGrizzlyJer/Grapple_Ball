using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    [SerializeField] private Transform station;

    private void Start()
    {
        if (!station) Debug.Log("Please set parent on " + gameObject.name);
    }

    public virtual void PrimaryUse()
    {
        Debug.Log("Primary use activated on " + gameObject.name);
    }

    public virtual void SecondaryUse()
    {
        Debug.Log("Secondary use activated on " + gameObject.name);
    }

    // For multiple abilities, allows us to disable one ability and enable another
    public virtual void EnableAbility()
    {
        Debug.Log("Enabling ability " + gameObject.name);
        transform.SetParent(station);
        gameObject.transform.position = station.position;
        gameObject.SetActive(true);
    }
    
    public virtual void DisableAbility()
    {
        Debug.Log("Enabling ability " + gameObject.name);
        transform.SetParent(station);
        gameObject.transform.position = station.position;
        gameObject.SetActive(false);
    }
}

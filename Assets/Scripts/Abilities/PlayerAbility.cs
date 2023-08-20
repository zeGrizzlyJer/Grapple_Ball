using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    [SerializeField] protected Transform station;

    protected virtual void Start()
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

    protected void DockAbility()
    {
        Debug.Log("Docking ability: " + gameObject.name);
        transform.SetParent(station);
        //transform.position = station.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // For multiple abilities, allows us to disable one ability and enable another
    public void EnableAbility()
    {
        Debug.Log("Enabling ability: " + gameObject.name);
        DockAbility();
        gameObject.SetActive(true);
    }
    
    public void DisableAbility()
    {
        Debug.Log("Disabling ability: " + gameObject.name);
        DockAbility();
        gameObject.SetActive(false);
    }
}

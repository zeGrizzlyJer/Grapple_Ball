using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStationPosition : MonoBehaviour
{
    private PlayerController pc;
    private Transform parent;
    private Transform playerInputSpace;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        parent = pc.transform;
        playerInputSpace = pc.playerInputSpace;
    }

    private void Update()
    {
        Vector3 forward = playerInputSpace.forward;
        transform.position = parent.position + forward * 0.5f;
        transform.rotation = playerInputSpace.rotation;
    }
}

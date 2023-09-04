using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float rotationSpeed;
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    
    void Update()
    {
        if (rotateX == true)
            transform.Rotate(new Vector3(rotationSpeed, 0, 0) * Time.deltaTime);

        if (rotateY == true)
            transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);

        if (rotateZ == true)
                transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
       
    }
}

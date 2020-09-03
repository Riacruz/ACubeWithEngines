using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThisObject : MonoBehaviour
{
    public float rotX = 0;
    public float rotY = 0;
    public float rotZ = 0;

    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotX*Time.deltaTime  , rotY * Time.deltaTime, rotZ * Time.deltaTime, Space.Self);
    }
}


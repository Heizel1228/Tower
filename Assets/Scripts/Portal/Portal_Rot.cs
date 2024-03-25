using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Portal_Rot : MonoBehaviour
{
    public Vector3 RotateAxis = new Vector3(1, 5, 10);

    void Update()
    {
        transform.Rotate(RotateAxis * Time.deltaTime);
    }
}

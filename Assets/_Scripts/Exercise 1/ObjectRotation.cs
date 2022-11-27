using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float maxSpeed = 600f;
    public Transform logoTransform;

    void Update()
    {
        logoTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed * maxSpeed;
    }
}

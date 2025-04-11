using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    GameObject cube;
    public Transform center;
    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;
    public float radius = 2.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;

    void Start()
    {
        
        
    }
    bool clicked;
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
        if (clicked)
        {

            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
        }
    }
}

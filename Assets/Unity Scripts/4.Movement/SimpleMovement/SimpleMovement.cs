using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    public Vector3 MoveDirection;
    public float speed;
    void Update ()
    {
        transform.position += MoveDirection * Time.deltaTime * speed;
	}
}

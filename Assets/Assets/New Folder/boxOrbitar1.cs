//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class boxOrbitar : MonoBehaviour
//{
//    public Transform pivot;
//    public Transform thisTransform;
//    private Quaternion DestRot = Quaternion.identity;

//    public float pivotDistance = 5;
//    public float rotationspeed = 10;
//    public float rotx = 0;
//    private float roty = 0;

//    public Vector2 MaxXRot;
//    private void Awake()
//    {
//        thisTransform = GetComponent<Transform>();
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }
//    bool canMove = false;
//    // Update is called once per frame
//    void Update()
//    {

//        if (Input.GetMouseButtonDown(0))
//        {
//            canMove = true;
//        }
//        if (Input.GetMouseButtonUp(0))
//        {
//            canMove = false;
//        }

//        if (canMove)
//        {
//            float horz = /*Input.GetAxis("Horizontal") */Input.GetAxis("Mouse X");
//            float vert = /*Input.GetAxis("Vertical");*/ Input.GetAxis("Mouse Y");


//            rotx += vert * Time.deltaTime * rotationspeed;
//            roty += horz * Time.deltaTime * rotationspeed;

//            Quaternion Yrot = Quaternion.Euler(0, roty, 0);
//            DestRot = Yrot * Quaternion.Euler(rotx, 0, 0);
//            thisTransform.rotation = DestRot;
//            thisTransform.position = pivot.position + thisTransform.rotation * Vector3.forward * -pivotDistance;
//        }
//    }
//}

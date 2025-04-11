using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotate : MonoBehaviour
{

    public Material skyMaterial;

    public float Speed;

    float counter;
    // Update is called once per frame
    void Update()
    {
        skyMaterial.SetFloat("_Rotation", counter);

        counter += Speed * Time.deltaTime;

        if (counter > 360) counter = 0;
    }
}

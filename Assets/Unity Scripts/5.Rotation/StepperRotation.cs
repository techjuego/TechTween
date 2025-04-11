using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepperRotation : MonoBehaviour 
{
	public Vector3 StepRotationAngle;
	public float TimeDelay;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (StepRotation ());
	}
	
	// Update is called once per frame
	IEnumerator StepRotation ()
	{
		transform.localEulerAngles += new Vector3 (StepRotationAngle.x, StepRotationAngle.y, StepRotationAngle.z);
		yield return new WaitForSeconds (TimeDelay);
		StartCoroutine (StepRotation ());
	}


}

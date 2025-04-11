using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongRotation : MonoBehaviour 
{
	public Vector3 RotationTocover;
	private Vector3 StartRotation;
	void Start () 
	{
		StartRotation = transform.position;
	}	

	void Update () 
	{
		transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * 10, RotationTocover.x),transform.localRotation.y, transform.localRotation.z);
	}
}

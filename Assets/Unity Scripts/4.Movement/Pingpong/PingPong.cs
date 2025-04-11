using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour 
{
	
	public Vector3 DisatanceToCover;

	void Update () 
	{		
		transform.position =   new Vector3 (Mathf.PingPong (Time.time, DisatanceToCover.x), transform.position.y, transform.position.z);
	}
}

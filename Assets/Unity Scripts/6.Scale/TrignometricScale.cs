using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrignometricScale : MonoBehaviour 
{
	public Vector3 Distance;
	public Vector3 ScaleFrequency;
	Vector3 NewScale;
	Vector3 StartScale;
	void Start()
	{
		StartScale = transform.localScale;
	}
	void Update()
	{
		NewScale.x = StartScale.x + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.x) * Distance.x;
		NewScale.y = StartScale.y + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.y) * Distance.y;
		NewScale.z = StartScale.z + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.z) * Distance.z;
		transform.localScale = new Vector3(NewScale.x, NewScale.y, NewScale.z);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrignometricRotationTan : MonoBehaviour {

	public Vector3 Distance;
	public Vector3 ScaleFrequency;
	Vector3 NewScale;
	Vector3 StartScale;
	void Start()
	{
		StartScale = transform.localEulerAngles;
	}
	void Update()
	{
		NewScale.x = StartScale.x + Mathf.Tan(Time.timeSinceLevelLoad * ScaleFrequency.x) * Distance.x;
		NewScale.y = StartScale.y + Mathf.Tan(Time.timeSinceLevelLoad * ScaleFrequency.y) * Distance.y;
		NewScale.z = StartScale.z + Mathf.Tan(Time.timeSinceLevelLoad * ScaleFrequency.z) * Distance.z;
		transform.localEulerAngles = new Vector3(NewScale.x, NewScale.y, NewScale.z);
	}
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
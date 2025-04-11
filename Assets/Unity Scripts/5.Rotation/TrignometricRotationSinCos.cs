using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrignometricRotationSinCos : MonoBehaviour {

	public Vector3 Distance;
	public Vector3 ScaleFrequency;
	Vector3 NewScale;
	Vector3 StartScale;
	void Start()
	{
		StartScale = transform.localEulerAngles;
	}
	public bool CosX, CosY, CosZ;
	void Update()
	{
		if (CosX) {
			NewScale.x = StartScale.x + Mathf.Cos(Time.timeSinceLevelLoad * ScaleFrequency.x) * Distance.x;
		} else {
			NewScale.x = StartScale.x + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.x) * Distance.x;
		}
		if (CosY) {
			NewScale.y = StartScale.y + Mathf.Cos(Time.timeSinceLevelLoad * ScaleFrequency.y) * Distance.y;
		} else {
			NewScale.y = StartScale.y + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.y) * Distance.y;
		}
		if (CosZ) {
			NewScale.z = StartScale.z + Mathf.Cos(Time.timeSinceLevelLoad * ScaleFrequency.z) * Distance.z;
		} else {
			NewScale.z = StartScale.z + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.z) * Distance.z;
		}


	
		transform.localEulerAngles = new Vector3(NewScale.x, NewScale.y, NewScale.z);
	}
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrignometricMovementSinCos : MonoBehaviour
{
    public Vector3 Distance;
    public Vector3 MovementFrequency;
    Vector3 Moveposition;
    Vector3 startPosition;
	public Color col;
	[Header("Tick For Cosine Movement ")]
	public bool xCos;
	public bool  yCos;
	public bool  zCos;
	public float GizmosSphereSize = 0.1f;
	public Vector3[] pos ;
    void Start()
    {
        startPosition = transform.position;	
    }
    void Update()
    {
		if (xCos) {
			Moveposition.x = startPosition.x + Mathf.Cos(Time.timeSinceLevelLoad * MovementFrequency.x) * Distance.x;
		} else {
			Moveposition.x = startPosition.x + Mathf.Sin(Time.timeSinceLevelLoad * MovementFrequency.x) * Distance.x;
		}
		if (yCos) {
			Moveposition.y = startPosition.y + Mathf.Cos(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
		} else {
			Moveposition.y = startPosition.y + Mathf.Sin(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
		}
		if (zCos) {
			Moveposition.z = startPosition.z + Mathf.Cos(Time.timeSinceLevelLoad * MovementFrequency.z) * Distance.z;
		} else {
			Moveposition.z = startPosition.z + Mathf.Sin(Time.timeSinceLevelLoad * MovementFrequency.z) * Distance.z;
		}
		transform.position = new Vector3(Moveposition.x, Moveposition.y, Moveposition.z);

    }
	void OnDrawGizmos()
	{
		Gizmos.color = col;
		Gizmos.DrawLine (Distance + startPosition,-Distance + startPosition);
		Gizmos.DrawSphere (Distance + startPosition, GizmosSphereSize);
		Gizmos.DrawSphere (startPosition, GizmosSphereSize);
		Gizmos.DrawSphere (-Distance + startPosition, GizmosSphereSize);

	}


}

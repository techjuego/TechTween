using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrignometricMovementTan : MonoBehaviour
{
    public Vector3 Distance;
    public Vector3 MovementFrequency;
    Vector3 Moveposition;
    Vector3 startPosition;
	public Color col;
	public float GizmosSphereSize = 0.1f;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
		Moveposition.x = startPosition.x + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.x) * Distance.x;
		Moveposition.y = startPosition.y + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
		Moveposition.z = startPosition.z + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.z) * Distance.z;
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

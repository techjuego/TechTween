using UnityEngine;
using System.Collections;

public class WaveRotate : MonoBehaviour
{

	Transform thisTrans;
	public float Speed = 3;
	public Vector3 RotationLimit;
	private Vector3 StartRot;
	void Start ()
	{
		StartRot = transform.localEulerAngles;
		thisTrans = transform;
		Invoke ("SetBreak", Random.Range (2, 5));
	}

	public	float timeCounter;
	public	Vector3 Rotations;

	void SetBreak ()
	{
		isOnBreak = true;
		Invoke ("ReleaseBreak", Random.Range (3.0f, 5.0f));

	}

	void ReleaseBreak ()
	{
		isOnBreak = false;
		Invoke ("SetBreak", Random.Range (1.0f, 2.0f));
	}


	bool isOnBreak = false;

	void Update ()
	{
		SineMovement ();
	}

	void SineMovement ()
	{
		if (isOnBreak)
			return;
		timeCounter += Time.deltaTime * Speed;
		Rotations.x = Mathf.Sin (timeCounter) * RotationLimit.x;
		Rotations.y = Mathf.Sin (timeCounter) * RotationLimit.y;
		Rotations.z = Mathf.Sin (timeCounter) * RotationLimit.z;

		thisTrans.localEulerAngles = StartRot + new Vector3 (Rotations.x, Rotations.y,Rotations.z);
	}
}

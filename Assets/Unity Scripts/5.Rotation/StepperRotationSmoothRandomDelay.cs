using UnityEngine;
using System.Collections;


public class StepperRotationSmoothRandomDelay : MonoBehaviour
{

	Transform thisTrans;
	public Vector3 Speed ;
	private Vector3 MovementLimit;
	private Vector3 StartPos;
	void Start ()
	{
		StartPos = transform.localEulerAngles;
		thisTrans = transform;
		Invoke ("SetBreak", Random.Range (2, 5));
	}

	private	float timeCounterx,timeCountery,timeCounterz;
	private	Vector3 movement;

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
		timeCounterx += Time.deltaTime * Speed.x;
		timeCountery += Time.deltaTime * Speed.y;
		timeCounterz += Time.deltaTime * Speed.z;
		movement.x = timeCounterx;
		movement.y = timeCountery;
		movement.z = timeCounterz;

		thisTrans.localEulerAngles = StartPos + new Vector3 (movement.x, movement.y,movement.z);
	}
}

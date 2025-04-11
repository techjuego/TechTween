using UnityEngine;
using System.Collections;

public class StepperRotateSmooth: MonoBehaviour
{

	Transform thisTrans;
	public Vector3 Speed;
	private Vector3 StartPos;
	public float MoveDelay;
	public float HaltDelay;
	void OnEnable ()
	{
		StartPos = transform.localEulerAngles;
		thisTrans = transform;
	
		ReleaseBreak ();
	}


	private	Vector3 rotations;

	void SetBreak ()
	{
		isOnBreak = true;
		Invoke ("ReleaseBreak", HaltDelay);

	}

	void ReleaseBreak ()
	{
		isOnBreak = false;
		Invoke ("SetBreak", MoveDelay);
	}


	bool isOnBreak = false;

	void Update ()
	{
		Movement ();
	}
	private float timerx,timery,timerz;
	void Movement ()
	{
		if (isOnBreak)
			return;	
		timerx += Time.deltaTime * Speed.x;
		timery += Time.deltaTime * Speed.y;
		timerz += Time.deltaTime * Speed.z;
		rotations.x = timerx;
		rotations.y =timery;
		rotations.z =timerz;

		thisTrans.localEulerAngles = StartPos + new Vector3 (rotations.x, rotations.y,rotations.z);
	}
}

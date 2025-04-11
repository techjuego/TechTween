using UnityEngine;
using System.Collections;

public class StepperMoveSmooth: MonoBehaviour
{

	Transform thisTrans;
	public Vector3 Speed;
	private Vector3 StartPos;
	public float MoveDelay;
	public float HaltDelay;
	void OnEnable ()
	{
		StartPos = transform.position;
		thisTrans = transform;
	
		ReleaseBreak ();
	}


	private	Vector3 movement;

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
		movement.x = timerx;
			movement.y =timery;
			movement.z =timerz;

		thisTrans.localPosition = StartPos + new Vector3 (movement.x, movement.y,movement.z);
	}
}

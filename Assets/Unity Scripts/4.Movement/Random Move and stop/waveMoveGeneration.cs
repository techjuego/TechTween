using UnityEngine;
using System.Collections;

public class waveMoveGeneration : MonoBehaviour
{

	Transform thisTrans;
	public float ModulationMul = 3;

	void Start ()
	{
		thisTrans = transform;
		Invoke ("SetBreak", Random.Range (2, 5));
	}

	public	float timeCounter;
	public	float movement;

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
		timeCounter += Time.deltaTime * ModulationMul;
		movement = Mathf.Sin (timeCounter) * 4.5f;
		thisTrans.localPosition = new Vector3 (movement, thisTrans.localPosition.y, thisTrans.localPosition.z);
	}
}

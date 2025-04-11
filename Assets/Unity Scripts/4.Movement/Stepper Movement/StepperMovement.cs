using UnityEngine;
using System.Collections; 
public class StepperMovement : MonoBehaviour {
	public Vector3 MovementDistacne;
	public float DeleyTime;
	void Start () 
	{
		StartCoroutine (StepMovements());
	}

	// Update is called once per frame
	IEnumerator StepMovements ()
	{
		transform.position += MovementDistacne;
		yield return new WaitForSeconds (DeleyTime);
		StartCoroutine (StepMovements ());
	}

}

using UnityEngine;
using System.Collections; 
public class StepperScale : MonoBehaviour {
	public Vector3 NewScale;
	public float DeleyTime;
	void Start () 
	{
		StartCoroutine (StepMovements());
	}
	// Update is called once per frame
	IEnumerator StepMovements ()
	{
		transform.localScale += NewScale;
		yield return new WaitForSeconds (DeleyTime);
		StartCoroutine (StepMovements());
	}
}

using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;


	void LateUpdate()
	{
		
		float wantedHeight = target.position.y + height;

		float currentHeight = transform.localPosition.y;

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		transform.localPosition = target.position;
		transform.localPosition -=  transform.forward * distance;
		// Set the height of the camera
		transform.localPosition = new Vector3(transform.localPosition.x, currentHeight, transform.localPosition.z);
		// Always look at the target
		transform.LookAt(target);
	}
}
using UnityEngine;
using System.Collections;

public class sphereTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}


	public int radius = 45;


	void Update ()
	{
		Collider[] hitColliders = Physics.OverlapSphere (transform.position + (transform.forward * radius), radius);
		int i = 0;
		while (i < hitColliders.Length) {
			 
			Debug.Log ("we got " + hitColliders [i].name);
			i++;
		}

		 
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position + (transform.forward * radius), radius);
	}
}

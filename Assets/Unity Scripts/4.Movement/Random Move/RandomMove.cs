using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {

	public Vector3 NextPos;
	public float lerpSpeed = 2;
	public Vector2 XLimit;
	public Vector2 YLimit;
	public Vector2 ZLimit;
	public float DelayTime =2;
	// Use this for initialization
	void Start ()
	{
		NextPos = new Vector3 (10,10,10);
		InvokeRepeating ("CalculateNextPos", 0, DelayTime);
	}
	float distance;
	float timecounter;
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector3.Lerp (transform.position, NextPos, Time.deltaTime * lerpSpeed);	
	}


	void CalculateNextPos()
	{
		NextPos = new Vector3 (Random.Range(XLimit.x,XLimit.y),Random.Range(YLimit.x,YLimit.y),Random.Range(ZLimit.x,ZLimit.y)); 	
	}
}

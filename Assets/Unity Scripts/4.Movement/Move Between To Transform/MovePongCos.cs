using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePongCos : MonoBehaviour {
	public Transform FirstPos;
	public Transform SecondPos;
	float starttime;
	public float movespeed;
	public float TimeSpeed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		starttime += Time.deltaTime * TimeSpeed;
		transform.localPosition = Vector3.Lerp (FirstPos.position,SecondPos.position,(Mathf.Sin(starttime * movespeed + Mathf.PI/2)+1)/2);
	
	}
}

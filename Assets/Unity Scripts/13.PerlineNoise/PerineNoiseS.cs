using UnityEngine;
using System.Collections;

public class smoothRandomNoise : MonoBehaviour
{

	public Transform thisTrans;

	void Start ()
	{
		thisTrans = transform;
	}

	// Update is called once per frame
	public float perlinVal;

	void Update ()
	{
		perlinVal = Mathf.PerlinNoise (0, Time.timeSinceLevelLoad).Remap (0, 1, -1, 1) / 4;

		thisTrans.localPosition = new Vector3 (thisTrans.localPosition.x, perlinVal, thisTrans.localPosition.z);
		thisTrans.localEulerAngles = new Vector3 (thisTrans.localEulerAngles.x, thisTrans.localEulerAngles.y, perlinVal * 8);
	}
}

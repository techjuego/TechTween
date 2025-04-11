using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class curveMove : MonoBehaviour
{
	public AnimationCurve curve;
	[Range (0, 1)]
	public float speed = 1;
	float incrementStep;
	public bool QuickSlideOnEnable = true;
	public Vector3 quickSlideDistance = new Vector3 (-3, 0, 0);
	void Update ()
	{
			transform.position = Vector3.Lerp (transform.position, quickSlideDistance, curve.Evaluate (incrementStep));
			incrementStep += speed * Time.deltaTime;
	}
}

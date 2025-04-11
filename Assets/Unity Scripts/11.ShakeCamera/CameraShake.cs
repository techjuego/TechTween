using UnityEngine;
using System.Collections;
using System;

public class CameraShake : MonoBehaviour
{

    public Transform camTransform;

    //	public GameObject DamageEffect;
    public bool canShake = false;
    //to camera shake time

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    //to camera shake amount
    public float decreaseFactor = 1.0f;
    //camera shake decrease amount
    public float BigShakeDecreaseFactor;
    Vector3 originalPos;
    //to camera position
    void Awake()
    {
#if UNITY_EDITOR
        Camera.main.farClipPlane = ForEditorDrawValue;
#else
  Camera.main.farClipPlane = ForDeviceDrawValue;
#endif
        //to getting camera object
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;

      
    }



    void OnDisable()
    {

        

    }

    void LateUpdate()
    {
        //for camera shake 
        if (canShake)
        {
            camTransform.localPosition = originalPos + (camTransform.InverseTransformDirection(transform.right) * UnityEngine.Random.Range(-1, 1) * shakeAmount);
        }
        else
        {
            camTransform.localPosition = Vector3.MoveTowards(camTransform.localPosition, originalPos, Time.deltaTime * 20);
        }
    }


    void CamerashakeBig(System.Object obj, EventArgs args)
    {
        canShake = true;
        //	DamageEffect.SetActive (true);
        StartCoroutine(AceHelper.waitThenCallback(decreaseFactor, () =>
        {
            //	DamageEffect.SetActive (false);

        }));
        StartCoroutine(AceHelper.waitThenCallback(decreaseFactor * 2, () =>
        {
            canShake = false;

        }));
#if UNITY_ANDROID && !UNITY_EDITOR
		Vibration.Vibrate (5);
#endif

    }


    void CamerashakeSmall(System.Object obj, EventArgs args)
    {
        canShake = true;
        //	DamageEffect.SetActive (true);
        StartCoroutine(AceHelper.waitThenCallback(decreaseFactor, () =>
        {
            canShake = false;
            //	DamageEffect.SetActive (false);
        }));
#if UNITY_ANDROID && !UNITY_EDITOR
		Vibration.Vibrate (5);
#endif
    }

}

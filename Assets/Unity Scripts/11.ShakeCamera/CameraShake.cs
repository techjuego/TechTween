using UnityEngine;
public class CameraShake : MonoBehaviour
{
    public bool canShake = false;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public float BigShakeDecreaseFactor;
    Vector3 originalPos;
    public Vector3 direction;
    void OnEnable()
    {
        originalPos = transform.localPosition;
        CamerashakeSmall();
    }
    void LateUpdate()
    {
        if (canShake)
        {
            transform.localPosition = originalPos + (transform.InverseTransformDirection(direction) * Random.Range(-1f, 1f) * shakeAmount);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, Time.deltaTime * 20);
        }
    }
    void CamerashakeBig()
    {
        canShake = true;
        StartCoroutine(AceHelper.waitThenCallback(decreaseFactor * 2, () =>
        {
            canShake = false;
        }));
    }
    void CamerashakeSmall()
    {
        canShake = true;
        StartCoroutine(AceHelper.waitThenCallback(decreaseFactor, () =>
        {
            canShake = false;
        }));
    }
}

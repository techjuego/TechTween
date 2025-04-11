using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInTime : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(MoveOverSeconds(gameObject, new Vector3(0, 0, 90), 10));
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingrot= objectToMove.transform.localEulerAngles;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.localEulerAngles = Vector3.Slerp(startingrot, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.localEulerAngles = end;
    }
}

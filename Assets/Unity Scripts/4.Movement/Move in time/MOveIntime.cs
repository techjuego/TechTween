using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOveIntime : MonoBehaviour {

    public Transform newPosition;
    public float timetomove;
    public TextMesh time;
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(MoveOverSeconds(gameObject,newPosition.position, timetomove));
	}
   
	void Update () {
   
        time.text = Mathf.RoundToInt( elapsedTime).ToString();
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
    float elapsedTime = 0;
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
       
        Vector3 startingPos = objectToMove.transform.position;
        
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }
}

 
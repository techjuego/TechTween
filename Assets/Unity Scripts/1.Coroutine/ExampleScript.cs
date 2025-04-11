using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExampleScript : MonoBehaviour
{


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            // this moves object to random position after some delay
            StartCoroutine(GameEventCoroutine.DelayFuntction(1f, () =>
            {
                transform.position = new Vector3(Random.Range(0, 5), 0.5f, Random.Range(0, 5));
            }));
        }
    }

    public void MoveObect()
    {
        StartCoroutine(GameEventCoroutine.DelayFuntction(1f, () =>
        {
            transform.position = new Vector3(Random.Range(0, 5), 0.5f, Random.Range(0, 5));
        }));
    }
    public Text Buttontext;
    public void ChangeText() {
        StartCoroutine(GameEventCoroutine.DelayFuntction(1,()=> {
            Buttontext.text = "Pressed";
        }));
    }
}

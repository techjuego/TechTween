using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEventCoroutine : MonoBehaviour
{

    public static IEnumerator DelayFuntction(float timedelay,Action operation)
    {
        yield return new WaitForSeconds(timedelay);
        operation();
    }

    // how to use this couroutine

    //you can access this coroutine in any class anywhere  

    //      StartCoroutine(GameEventCoroutine.DelayFuntction(1f, () =>
    //      {
    //          anything you want to perform after delay   
    //      }));   

    //Example1
    //  void GameOver()
    //  {
    //      StartCoroutine(GameEventCoroutine.DelayFuntction(1f, () =>
    //      {
    //          showui();
    //showad();    
    //      }));  
    //  }
    //Example2
    //  private void OnTriggerEnter(Collider other)
    //  {
    //      if (other.name.Contains("Player"))
    //     {
    //          StartCoroutine(GameEventCoroutine.DelayFuntction(1f, () =>
    //              {
    //                  gameObject.setactive(false); 
    //              })); 
    //     }
    //  }
}

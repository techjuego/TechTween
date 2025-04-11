using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DayType
{
    Night,
    Day,
    None
};
public class CubemapSkyboxLerp : MonoBehaviour {
 
    public DayType dtype;  
  
    [Header("Max value means slower time to lerp")]
    public float timetochange = 2;
	void Start ()
    {
        switch (dtype)
        {
            case DayType.Night:               
                RenderSettings.skybox.SetFloat("_LerpValue", 0);
                break;
            case DayType.Day:             
                RenderSettings.skybox.SetFloat("_LerpValue", 1);
              
                break;
          
        }
    }
    float t,LerpValue,RotationValue;
	void Update ()
    {
              
        switch (dtype)
        {
            case DayType.Night:
                if (LerpValue <= 0) return;
                t += Time.deltaTime / timetochange;
                LerpValue = Mathf.Lerp(1, 0, t);
                RenderSettings.skybox.SetFloat("_LerpValue", LerpValue);
                if (LerpValue <= 0)
                {
                    dtype = DayType.None;
                }
                break;
            case DayType.Day:
                if (LerpValue >= 1) return;
                t += Time.deltaTime / timetochange;
                LerpValue = Mathf.Lerp(0, 1, t);
                RenderSettings.skybox.SetFloat("_LerpValue", LerpValue);
                if (LerpValue >= 1f)
                {
                    dtype = DayType.None;
                }
                break;
            case DayType.None:
                t = 0;
                break;
        }
       
	}
}

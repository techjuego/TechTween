using TechJuego.Tween;
using UnityEngine;

public class TrigSinValue : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimTrigFloat(gameObject, 0f, 1f, 1f).SetOnUpdateFloat(v => Debug.Log(v));
    }
}

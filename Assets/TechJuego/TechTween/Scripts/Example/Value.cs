using TechJuego.Tween;
using UnityEngine;

public class Value : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimFloat(gameObject, 0f, 1f, 1f).SetOnUpdateFloat(v => Debug.Log(v));
    }
}

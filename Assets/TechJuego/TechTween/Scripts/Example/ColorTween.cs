using TechJuego.Tween;
using UnityEngine;

public class ColorTween : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimColor(gameObject, UnityEngine.Color.red, 1f);
    }
}

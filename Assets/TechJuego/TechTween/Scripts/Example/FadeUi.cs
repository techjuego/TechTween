using TechJuego.Tween;
using UnityEngine;

public class FadeUi : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimCanvasAlpha(gameObject, 1, 0, 1f);
    }
}

using TechJuego.Tween;
using UnityEngine;

public class ShakeScale : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimShakeScale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1f);
    }
}

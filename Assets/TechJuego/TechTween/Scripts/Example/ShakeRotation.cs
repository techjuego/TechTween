using TechJuego.Tween;
using UnityEngine;

public class ShakeRotation : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimShakeRotation(gameObject, new Vector3(0, 15, 0), 1f);
    }
}

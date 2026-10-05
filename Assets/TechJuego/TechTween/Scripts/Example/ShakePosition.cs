using TechJuego.Tween;
using UnityEngine;

public class ShakePosition : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimShakePosition(gameObject, new Vector3(0.5f, 0.5f, 0), 1f);
    }
}

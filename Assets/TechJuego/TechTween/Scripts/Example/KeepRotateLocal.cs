using TechJuego.Tween;
using UnityEngine;

public class KeepRotateLocal : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimContinuousRotation(gameObject, Vector3.up, 90f).SetLocal();
    }
}

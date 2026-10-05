using TechJuego.Tween;
using UnityEngine;

public class PunchRotationLocal : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPunchLocalRotation(gameObject, new Vector3(0, 45, 0), 1f);
    }
}

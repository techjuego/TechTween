using TechJuego.Tween;
using UnityEngine;

public class PunchScale : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPunchScale(gameObject, new Vector3(0.2f, 0.2f, 0.2f), 1f);
    }
}

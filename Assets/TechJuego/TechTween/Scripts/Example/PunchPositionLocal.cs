using TechJuego.Tween;
using UnityEngine;

public class PunchPositionLocal : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPunchLocalPosition(gameObject, new Vector3(1, 1, 0), 1f);
    }
}

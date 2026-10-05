using TechJuego.Tween;
using UnityEngine;

public class PunchPosition : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPunchPosition(gameObject, new Vector3(1, 1, 0), 1f);
    }
}

using TechJuego.Tween;
using UnityEngine;

public class PunchRotation : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPunchRotation(gameObject, new Vector3(0, 45, 0), 1f);
    }
}

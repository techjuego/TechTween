using TechJuego.Tween;
using UnityEngine;

public class Look : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimLookAt(gameObject, Vector3.zero, 1f);
    }
}

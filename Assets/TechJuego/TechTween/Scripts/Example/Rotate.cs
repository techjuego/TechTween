using TechJuego.Tween;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimRotation(gameObject, new Vector3(0, 90, 0), 1f);
    }
}

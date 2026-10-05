using TechJuego.Tween;
using UnityEngine;

public class TrigRotate : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimTrigRotation(gameObject, new Vector3(0, 45, 0), new Vector3(0, 1, 0));
    }
}

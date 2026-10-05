using TechJuego.Tween;
using UnityEngine;

public class TrigScale : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimTrigScale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1, 1, 1));
    }
}

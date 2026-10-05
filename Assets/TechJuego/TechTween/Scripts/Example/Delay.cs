using TechJuego.Tween;
using UnityEngine;

public class Delay : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.DelayCall(gameObject, 1f, () => Debug.Log("Delay complete!"));
    }
}

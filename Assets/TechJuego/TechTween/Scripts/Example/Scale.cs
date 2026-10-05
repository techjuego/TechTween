using TechJuego.Tween;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimScale(gameObject, new Vector3(2, 2, 2), 1f);
    }
}

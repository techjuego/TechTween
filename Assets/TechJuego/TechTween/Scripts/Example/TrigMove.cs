using TechJuego.Tween;
using UnityEngine;

public class TrigMove : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimTrigPosition(gameObject, new Vector3(1, 0, 0), new Vector3(1, 0, 0)).ShowMovementPath();
    }
}

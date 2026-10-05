using TechJuego.Tween;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 endPos;
    public Vector3 arc;
    private void OnEnable()
    {
        TechTween.AnimArcPosition(gameObject, endPos, arc, 3).ShowMovementPath().SetEaseType(EaseTween.EaseInBounce).SetPingPong(true);
    }
}

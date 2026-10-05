using TechJuego.Tween;
using UnityEngine;

public class TrigMoveTransform : MonoBehaviour
{
    public Transform firstPos;
    public Transform secondPos;
    private void OnEnable()
    {
        TechTween.AnimTrigPositionTransform(gameObject, firstPos, secondPos, 1f, 1f).SetInfiniteLoop(true).ShowMovementPath();
    }
}

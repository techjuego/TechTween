using TechJuego.Tween;
using UnityEngine;

public class MoveLocal : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPosition(gameObject, new Vector3(2, 2, 2), 1f).SetLocal().ShowMovementPath();
    }
}

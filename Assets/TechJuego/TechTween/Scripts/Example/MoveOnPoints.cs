using TechJuego.Tween;
using UnityEngine;

public class MoveOnPoints : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimPath(gameObject, new Vector3[] { Vector3.zero, Vector3.one, Vector3.right }, 2f).ShowMovementPath();
    }
}

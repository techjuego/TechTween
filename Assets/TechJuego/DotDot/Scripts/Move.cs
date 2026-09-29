using System.Collections;
using System.Collections.Generic;
using TechJuego.Tween;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 endPos;
    public Vector3 arc;
    private void OnEnable()
    {
        TechTween.AnimPosition(gameObject, endPos,5).ShowMovementPath(Color.green,true);
    }
}

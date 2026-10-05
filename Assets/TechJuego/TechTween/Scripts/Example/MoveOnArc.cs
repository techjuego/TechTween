using System.Collections;
using System.Collections.Generic;
using TechJuego.Tween;
using UnityEngine;

public class MoveOnArc : MonoBehaviour
{
    public Vector3 endPosition;
    public Vector3 arkValue;
    public float time;
    private void OnEnable()
    {
        TechTween.AnimArcPosition(gameObject, endPosition, arkValue, time).ShowMovementPath();
    }
}

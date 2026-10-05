using TechJuego.Tween;
using UnityEngine;

public class SpriteRendererAlpha : MonoBehaviour
{
    private void OnEnable()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>(); if (sr != null) TechTween.AnimSpriteAlpha(sr, 0f, 1f);
    }
}

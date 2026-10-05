using TechJuego.Tween;
using UnityEngine;

public class CanvasGroupAlpha : MonoBehaviour
{
    private void OnEnable()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>(); if (cg != null) TechTween.AnimCanvasGroupAlpha(cg, 0f, 1f);
    }
}

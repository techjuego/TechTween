using UnityEngine;
using TechJuego;

public class Test1 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void OnEnable()
    {
        TechTween.SpriteRendererAlpha(spriteRenderer, 0, 1);
    }
}

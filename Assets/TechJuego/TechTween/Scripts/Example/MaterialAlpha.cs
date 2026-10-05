using TechJuego.Tween;
using UnityEngine;

public class MaterialAlpha : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimMaterialAlpha(gameObject, 0f, 1f);
    }
}

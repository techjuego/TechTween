using TechJuego.Tween;
using UnityEngine;

public class AudioTween : MonoBehaviour
{
    private void OnEnable()
    {
        TechTween.AnimAudioVolume(gameObject, 1f, 1f);
    }
}

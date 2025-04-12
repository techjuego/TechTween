using UnityEngine;
using TechJuego;
public class ScaleExample : MonoBehaviour
{
    public Vector3 Scale;
    public float time;
    public EaseTween easeTween;
    private void OnEnable()
    {
        //TechTween.ScaleTo(gameObject, Scale, time).SetEaseType(easeTween);
        TechTween.ScaleFrom(gameObject, Scale, time).SetEaseType(easeTween);
        TechTween.DelayCall(gameObject, 1, () => { Debug.Log(">>>"); });
    }
}

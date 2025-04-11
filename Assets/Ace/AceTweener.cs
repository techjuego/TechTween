using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public enum TweenerType
{
    Position,
    Scale,
    UIPosition

}


public class AceTweener : MonoBehaviour
{
    public TweenerType selectedType;
    [SerializeField]
    public bool PlayTweenOnStart = true;
    public Vector3 tweenBegin;
    public Vector3 tweenFinish;
    public float startTime;
    public float timetoFinishTween = 1;
    public float delayTime;
    public Ease ease = Ease.EaseOutBounce;
    Transform thisTransfrom;
    int enabledCount = 0;
    public bool UseSlidersForPosition = true;
    [Range(-50, 50)]
    public float OutX = 1;
    [Range(-50, 50)]
    public float OutY = 1;
    public bool useScaleAmount = true;
    [Range(1, 25)]
    public float ScaleAmount = 1;
    public bool loopTween = false;
    [Range(1, 25)]
    public int tweenRepeatCount;
    int tweenRepeat;
    public bool canDisableScript = false;
    public bool canSendMessageOnComplete = false;
    public UnityEvent UnityAction;
    public RectTransform rect;
    public void OnEnable()
    {
        tweenRepeat = tweenRepeatCount;
        thisTransfrom = this.transform;
        if (enabledCount != 0)
        {
            Start();
        }
        enabledCount++;
    }
    float ZeroX, ZeroY;
    float lastTime;
    public float TweenRepeatDelay = 0.1f;
    public void OnValidate()
    {
        if (rect == null && selectedType == TweenerType.Position)
        {
            if (this.GetComponent<RectTransform>() != null)
                rect = this.GetComponent<RectTransform>();
        }

        if (rect != null && enabledCount == 0 && PlayTweenOnStart)
        {

            tweenFinish = rect.anchoredPosition;
            ZeroX = tweenFinish.x;
            ZeroY = tweenFinish.y;
            if (ZeroX == 0)
            {
                ZeroX = 100;
            }
            if (ZeroY == 0)
            {
                ZeroY = 100;
            }
            tweenBegin = new Vector3(ZeroX * OutX, ZeroY * OutY, transform.localPosition.z);
        }
        if (selectedType == TweenerType.Scale)
        {
            tweenBegin = transform.localScale * ScaleAmount;
            tweenFinish = transform.localScale;
            rect = null;
        }
    }


    public void Start()
    {
        if (selectedType == TweenerType.Position && UseSlidersForPosition && rect == null)
        {
            if (rect == null)
            {
                tweenFinish = thisTransfrom.position;
            }
            else
            {
                tweenFinish = rect.anchoredPosition;
            }
            tweenBegin = new Vector3(tweenFinish.x * OutX, tweenFinish.y * OutY, tweenFinish.z);
            thisTransfrom.position = tweenBegin;

        }
        else if (useScaleAmount && selectedType == TweenerType.Scale && enabledCount == 1 && rect == null)
        {

            tweenBegin = thisTransfrom.localScale * ScaleAmount;
            tweenFinish = thisTransfrom.localScale;
            if (PlayTweenOnStart)
                thisTransfrom.localScale = tweenBegin;

        }
        else if (rect && enabledCount == 1)
        {

            OnValidate();
            rect.anchoredPosition = tweenBegin;
        }
        if (PlayTweenOnStart)
        {
            beginTween();
        }
    }
    public void beginTween()
    {
        if (Time.timeSinceLevelLoad - lastTime < TweenRepeatDelay)
            return;
        PlayTweenOnStart = true;
        lastTime = Time.timeSinceLevelLoad;
        startTime = Time.time;
        canSendMessageOnComplete = true;
    }
    void Update()
    {
        if (!PlayTweenOnStart)
            return;
        Vector3 begin = tweenBegin;
        Vector3 finish = tweenFinish;
        Vector3 change = finish - begin;
        float duration = timetoFinishTween;
        float currentTime = Time.time - (startTime + delayTime);
        if (duration == 0)
        {
            thisTransfrom.position = finish;
            return;
        }
        if (Time.time > startTime + delayTime + timetoFinishTween)
        {
            switch (selectedType)
            {
                case TweenerType.Position:
                    if (rect == null)
                    {
                        thisTransfrom.position = tweenFinish;
                    }
                    else
                    {
                        rect.anchoredPosition = tweenFinish;
                    }
                    break;
                case TweenerType.Scale:
                    thisTransfrom.localScale = tweenFinish;
                    break;
            }
            if (loopTween)
            {
                beginTween();
            }
            else if (tweenRepeat != 0)
            {
                beginTween();
                tweenRepeat--;
            }
            if (canSendMessageOnComplete && tweenRepeat == 0)
            {
                UnityAction.Invoke();
                canSendMessageOnComplete = false;
            }
            if (canDisableScript && tweenRepeatCount == 0)
            {
                this.enabled = false;
            }
            return;
        }
        if (Time.time > startTime + delayTime)
        {
            switch (selectedType)
            {
                case TweenerType.Position:
                    if (rect == null)
                    {
                        thisTransfrom.position = Equations.ChangeVector(currentTime, begin, change, duration, ease);
                    }
                    else
                    {
                        rect.anchoredPosition = Equations.ChangeVector(currentTime, begin, change, duration, ease);
                    }
                    break;
                case TweenerType.Scale:
                    thisTransfrom.localScale = Equations.ChangeVector(currentTime, begin, change, duration, ease);
                    break;
            }
            return;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TechJuego.TileMatchMania.Utils
{
    public class TweenEvent
    {
        public delegate void OnAction(GameObject gameObject);
        public static OnAction CancleTween;
    }
    public static class CoroutineExtensions
    {
        public static Coroutine RunCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
        {
            return monoBehaviour.StartCoroutine(routine);
        }
    }
    public enum OperationType
    {
        None,
        Tween,
        CallInSec,
    }
    public enum TweenType
    {
        Delay,
        Value,
        Move,
        MoveLocal,
        Scale,
        Rotate,
        RotateLocal,
        KeepRotate,
        KeepRotateLocal,
        TrigRotate,
        TrigScale,
        TrigMove,
        TrigSinValue,
        CanvasGroupAlpha,
        SpriteRendererAlpha,
        FadeUi,
        MaterialAlpha,
        TrigMoveTransform,
        MoveOnPoints,
        PunchScale,
        PunchPosition,
        PunchPositionLocal,
        PunchRotation,
        PunchRotationLocal,
        ShakePosition,
        ShakeScale,
        ShakeRotation,
        Color,
        Audio,
        Look
    }
    public enum EaseTween
    {
        Linear,
        EaseInQuad, EaseOutQuad, EaseInOutQuad, EaseOutInQuad,
        EaseInCubic, EaseOutCubic, EaseInOutCubic, EaseOutInCubic,
        EaseInQuart, EaseOutQuart, EaseInOutQuart, EaseOutInQuart,
        EaseInQuint, EaseOutQuint, EaseInOutQuint, EaseOutInQuint,
        EaseInSine, EaseOutSine, EaseInOutSine, EaseOutInSine,
        EaseInExpo, EaseOutExpo, EaseInOutExpo, EaseOutInExpo,
        EaseInCirc, EaseOutCirc, EaseInOutCirc, EaseOutInCirc,
        EaseInBounce, EaseOutBounce, EaseInOutBounce, EaseOutInBounce,
        EaseInBack, EaseOutBack, EaseInOutBack, EaseOutInBack,
        EaseInElastic, EaseOutElastic, EaseInOutElastic, EaseOutInElastic,
        EaseSpring,
        SmoothStep,
        SmootherStep
    }
    public class TweenUpdate
    {
        public Action OnTweenStart;
        public Action OnTweenComplete;
        public Action<float> onUpdateValue;
        public Action<int> onUpdateIntValue;
        public Action<Vector2> onUpdateVector2;
        public Action<Vector3> onUpdateVector3;
        public void Reset()
        {
            OnTweenStart = null;
            onUpdateValue = null;
            onUpdateVector2 = null;
            onUpdateVector3 = null;
            OnTweenComplete = null;
            onUpdateIntValue = null;
        }
    }
  
    public class TechTween : MonoBehaviour
    {
        private void OnEnable()
        {
            TweenEvent.CancleTween += TweenEvent_CancleTween;
        }
        private void TweenEvent_CancleTween(GameObject gameObject)
        {
            if (gameObject == null) return;
            TechTween[] techTweens = gameObject.GetComponentsInChildren<TechTween>();
            for (int i = 0; i < techTweens.Length; i++)
            {
                if (techTweens[i] != null)
                    Destroy(techTweens[i]);
            }
        }
        private void OnDisable()
        {
            TweenEvent.CancleTween -= TweenEvent_CancleTween;
        }
        class UpdateLayout
        {
            public UpdateLayout() { }
            public HorizontalLayoutGroup horizontalLayout;
            public VerticalLayoutGroup verticalLayout;
            public GridLayoutGroup gridLayoutGroup;
            public IEnumerator RunTween()
            {
                yield return new WaitForEndOfFrame();
                if (horizontalLayout != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(horizontalLayout.GetComponent<RectTransform>());
                }
                if (verticalLayout != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayout.GetComponent<RectTransform>());
                }
                if (gridLayoutGroup != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayoutGroup.GetComponent<RectTransform>());
                }
            }
        }
        public static void UpdateLayputGroup(MonoBehaviour mono, HorizontalLayoutGroup hgroup)
        {
            UpdateLayout tween = new UpdateLayout();
            tween.horizontalLayout = hgroup;
            mono.RunCoroutine(tween.RunTween());
        }
        public static void UpdateLayputGroup(MonoBehaviour mono, VerticalLayoutGroup hgroup)
        {
            UpdateLayout tween = new UpdateLayout();
            tween.verticalLayout = hgroup;
            mono.RunCoroutine(tween.RunTween());
        }
        public static void UpdateLayputGroup(MonoBehaviour mono, GridLayoutGroup ggroup)
        {
            UpdateLayout tween = new UpdateLayout();
            tween.gridLayoutGroup = ggroup;
            mono.RunCoroutine(tween.RunTween());
        }
        class FrameEnd
        {
            public Action OnComplete;
            public FrameEnd() { }
            public IEnumerator RunTween()
            {
                yield return new WaitForEndOfFrame();
                OnComplete?.Invoke();
            }
        }
        public static void CallAfterFrameEnd(MonoBehaviour mono, Action OnComplete)
        {
            FrameEnd tween = new FrameEnd();
            tween.OnComplete = OnComplete;
            mono.RunCoroutine(tween.RunTween());
        }
        public class DelayDetail
        {
            public float time;
            public Action OnComplete;
            public DelayDetail() { }
            public IEnumerator RunTween()
            {
                yield return new WaitForSeconds(time);
                OnComplete?.Invoke();
            }
        }
        [HideInInspector]
        public TweenDetail tweenDetail;
        public static bool IsPaused { get; private set; }

        public static void PauseAllTweens()
        {
            IsPaused = true;
            TechTween[] allTweens = FindObjectsOfType<TechTween>();
            foreach (var tween in allTweens)
            {
                if (tween != null && tween.tweenDetail != null)
                {
                    tween.tweenDetail.Pause();
                }
            }
        }

        public static void ResumeAllTweens()
        {
            IsPaused = false;
            TechTween[] allTweens = FindObjectsOfType<TechTween>();
            foreach (var tween in allTweens)
            {
                if (tween != null && tween.tweenDetail != null)
                {
                    tween.tweenDetail.Resume();
                }
            }
        }

        private void Update()
        {
            if (IsPaused) return;
            if (tweenDetail == null) return;
            switch (tweenDetail.tweenType)
            {
                case TweenType.CanvasGroupAlpha:
                case TweenType.SpriteRendererAlpha:
                case TweenType.Value:
                case TweenType.Move:
                case TweenType.MoveLocal:
                case TweenType.Scale:
                case TweenType.Rotate:
                case TweenType.Delay:
                case TweenType.FadeUi:
                case TweenType.MaterialAlpha:
                case TweenType.MoveOnPoints:
                case TweenType.PunchScale:
                case TweenType.PunchPosition:
                case TweenType.PunchPositionLocal:
                case TweenType.PunchRotation:
                case TweenType.PunchRotationLocal:
                case TweenType.ShakePosition:
                case TweenType.ShakeScale:
                case TweenType.ShakeRotation:
                case TweenType.Color:
                case TweenType.Audio:
                case TweenType.Look:
                    tweenDetail.UpdateTween();
                    break;
                case TweenType.KeepRotate:
                case TweenType.KeepRotateLocal:
                    tweenDetail.ContinueUpdateTween();
                    break;
                case TweenType.TrigRotate:
                case TweenType.TrigScale:
                case TweenType.TrigMove:
                case TweenType.TrigSinValue:
                case TweenType.TrigMoveTransform:
                    tweenDetail.UpdateTrignometric();
                    break;
            }
        }
        public static void CancleTween(GameObject gameObject)
        {
            if (gameObject == null) return;
            TechTween[] techTweens = gameObject.GetComponentsInChildren<TechTween>();
            for (int i = 0; i < techTweens.Length; i++)
            {
                if (techTweens[i] != null)
                    Destroy(techTweens[i]);
            }
        }
        public static TweenDetail CallInSec(GameObject gameObject, int count, Action onComplete)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.repeat = Mathf.Max(1, count);
            tween.action = onComplete;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartAction();
            return tween;
        }
        public static void DelayCall(GameObject gameObject, float time, Action OnComplete)
        {
            TweenDetail tween = new TweenDetail();
            tween.time = time;
            tween.action = OnComplete;
            tween.tweenType = TweenType.Delay;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
        }
        public static TweenDetail MoveToArc(GameObject gameObject, Vector3 startPoint, Vector3 endPoint, Vector3 arcHeight, float time)
        {
            if (gameObject == null) return null;
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = startPoint;
            tween.to = endPoint;
            tween.arcHeight = arcHeight;
            tween.time = time;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartJump();
            return tween;
        }
        public static TweenDetail MoveToArc(GameObject gameObject, Vector3 endPoint, Vector3 arcHeight, float time)
        {
            if (gameObject == null) return null;
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.to = endPoint;
            tween.arcHeight = arcHeight;
            tween.time = time;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartJump();
            return tween;
        }
        public static TweenDetail MoveToArc(RectTransform rect, Vector3 endPoint, Vector3 arcHeight, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.rectTrans = rect;
            tween.from = rect.anchoredPosition;
            tween.to = endPoint;
            tween.arcHeight = arcHeight;
            tween.time = time;
            tween.techTween = rect.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartJump();
            return tween;
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, Vector3[] points, float time, bool loop = false)
        {
            if (gameObject == null) return null;
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.pathPoints = new List<Vector3>(points);
            if (loop) 
            {
                tween.pathPoints.Add(tween.from);
                tween.SetInfiniteLoop(true);
            }
            tween.time = time;
            tween.tweenType = TweenType.MoveOnPoints;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail MoveRandomBezier(GameObject gameObject, Vector3 endPoint, float time, float randomness = 0.5f)
        {
            if (gameObject == null) return null;
            Vector3 startPoint = gameObject.transform.position;
            
            // Randomly pick a control point for the quadratic Bezier curve
            Vector3 midPoint = (startPoint + endPoint) / 2f;
            float distance = Vector3.Distance(startPoint, endPoint);
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * (distance * randomness);
            Vector3 controlPoint = midPoint + randomOffset;
            
            int segments = 20; // Number of points to make the curve smooth
            Vector3[] pathPoints = new Vector3[segments];
            for (int i = 1; i <= segments; i++)
            {
                float t = i / (float)segments;
                // Quadratic bezier formula
                Vector3 position = (1 - t) * (1 - t) * startPoint + 2 * (1 - t) * t * controlPoint + t * t * endPoint;
                pathPoints[i - 1] = position;
            }
            
            return MoveOnPoints(gameObject, pathPoints, time, false);
        }

        public static TweenDetail MoveRandomBezier(RectTransform rect, Vector3 endPoint, float time, float randomness = 0.5f)
        {
            if (rect == null) return null;
            Vector3 startPoint = rect.position;
            
            Vector3 midPoint = (startPoint + endPoint) / 2f;
            float distance = Vector3.Distance(startPoint, endPoint);
            // Use 2D random circle for UI
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * (distance * randomness);
            Vector3 controlPoint = midPoint + new Vector3(randomOffset.x, randomOffset.y, 0f);
            
            int segments = 20;
            Vector3[] pathPoints = new Vector3[segments];
            for (int i = 1; i <= segments; i++)
            {
                float t = i / (float)segments;
                Vector3 position = (1 - t) * (1 - t) * startPoint + 2 * (1 - t) * t * controlPoint + t * t * endPoint;
                pathPoints[i - 1] = position;
            }
            
            return MoveOnPoints(rect, pathPoints, time, false);
        }

        // ParticleSystem Overload - Moves INDIVIDUAL PARTICLES
        public static TweenDetail MoveRandomBezier(ParticleSystem particle, Vector3 endPoint, float time, float randomness = 0.5f)
        {
            if (particle == null) return null;
            
            // Setup individual particle updater
            ParticleBezierUpdater updater = particle.gameObject.GetComponent<ParticleBezierUpdater>();
            if (updater == null) updater = particle.gameObject.AddComponent<ParticleBezierUpdater>();
            
            updater.ps = particle;
            updater.targetPosition = endPoint;
            updater.randomness = randomness;
            
            var main = particle.main;
            main.startLifetime = time; // Match particle life to the requested tween time
            
            return new TweenDetail(); // Return empty detail to match signature
        }

        // Transform Overload
        public static TweenDetail MoveRandomBezier(Transform transform, Vector3 endPoint, float time, float randomness = 0.5f)
        {
            if (transform == null) return null;
            return MoveRandomBezier(transform.gameObject, endPoint, time, randomness);
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, List<Vector3> points, float time, bool loop = false)
        {
            return MoveOnPoints(gameObject, points.ToArray(), time, loop);
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, Transform[] points, float time, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i].position;
            return MoveOnPoints(gameObject, vectors, time, loop);
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, List<Transform> points, float time, bool loop = false)
        {
            return MoveOnPoints(gameObject, points.ToArray(), time, loop);
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, Vector2[] points, float time, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i];
            return MoveOnPoints(gameObject, vectors, time, loop);
        }

        public static TweenDetail MoveOnPoints(GameObject gameObject, List<Vector2> points, float time, bool loop = false)
        {
            return MoveOnPoints(gameObject, points.ToArray(), time, loop);
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, Vector3[] points, float time, bool loop = false)
        {
            if (rect == null) return null;
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.rectTrans = rect;
            tween.from = rect.position;
            tween.pathPoints = new List<Vector3>(points);
            if (loop) 
            {
                tween.pathPoints.Add(tween.from);
                tween.SetInfiniteLoop(true);
            }
            tween.time = time;
            tween.tweenType = TweenType.MoveOnPoints;
            tween.techTween = rect.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, List<Vector3> points, float time, bool loop = false)
        {
            return MoveOnPoints(rect, points.ToArray(), time, loop);
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, Transform[] points, float time, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i].position;
            return MoveOnPoints(rect, vectors, time, loop);
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, List<Transform> points, float time, bool loop = false)
        {
            return MoveOnPoints(rect, points.ToArray(), time, loop);
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, Vector2[] points, float time, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i];
            return MoveOnPoints(rect, vectors, time, loop);
        }

        public static TweenDetail MoveOnPoints(RectTransform rect, List<Vector2> points, float time, bool loop = false)
        {
            return MoveOnPoints(rect, points.ToArray(), time, loop);
        }

        public static float CalculatePathTime(Vector3 start, Vector3[] points, float speed, bool loop)
        {
            float distance = 0f;
            Vector3 current = start;
            for (int i = 0; i < points.Length; i++)
            {
                distance += Vector3.Distance(current, points[i]);
                current = points[i];
            }
            if (loop && points.Length > 0)
            {
                distance += Vector3.Distance(current, start);
            }
            return speed > 0 ? distance / speed : 0;
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, Vector3[] points, float speed, bool loop = false)
        {
            if (gameObject == null) return null;
            float time = CalculatePathTime(gameObject.transform.position, points, speed, loop);
            return MoveOnPoints(gameObject, points, time, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, List<Vector3> points, float speed, bool loop = false)
        {
            if (gameObject == null) return null;
            float time = CalculatePathTime(gameObject.transform.position, points.ToArray(), speed, loop);
            return MoveOnPoints(gameObject, points, time, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, Transform[] points, float speed, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i].position;
            return MoveOnPointsSpeed(gameObject, vectors, speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, List<Transform> points, float speed, bool loop = false)
        {
            return MoveOnPointsSpeed(gameObject, points.ToArray(), speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, Vector2[] points, float speed, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i];
            return MoveOnPointsSpeed(gameObject, vectors, speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(GameObject gameObject, List<Vector2> points, float speed, bool loop = false)
        {
            return MoveOnPointsSpeed(gameObject, points.ToArray(), speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, Vector3[] points, float speed, bool loop = false)
        {
            if (rect == null) return null;
            float time = CalculatePathTime(rect.position, points, speed, loop);
            return MoveOnPoints(rect, points, time, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, List<Vector3> points, float speed, bool loop = false)
        {
            if (rect == null) return null;
            float time = CalculatePathTime(rect.position, points.ToArray(), speed, loop);
            return MoveOnPoints(rect, points, time, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, Transform[] points, float speed, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i].position;
            return MoveOnPointsSpeed(rect, vectors, speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, List<Transform> points, float speed, bool loop = false)
        {
            return MoveOnPointsSpeed(rect, points.ToArray(), speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, Vector2[] points, float speed, bool loop = false)
        {
            Vector3[] vectors = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) vectors[i] = points[i];
            return MoveOnPointsSpeed(rect, vectors, speed, loop);
        }

        public static TweenDetail MoveOnPointsSpeed(RectTransform rect, List<Vector2> points, float speed, bool loop = false)
        {
            return MoveOnPointsSpeed(rect, points.ToArray(), speed, loop);
        }
        public static TweenDetail MoveTo(GameObject gameObject, Vector3 to, float time, AnimationCurve animationCurve)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.to = to;
            tween.time = time;
            tween.animationCurve = animationCurve;
            tween.tweenType = TweenType.Move;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail MoveTo(GameObject gameObject, Vector3 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Move;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail MoveTo(RectTransform rect, Vector3 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.rectTrans = rect;
            tween.from = rect.position;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Move;
            tween.techTween = rect.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail Rotate(GameObject gameObject, Vector3 axis, float speed)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.axis = axis;
            tween.speed = speed;
            tween.tweenType = TweenType.KeepRotate;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail TrignometricRotate(GameObject gameObject, Vector3 angleLimit, Vector3 frequency)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            tween.to = angleLimit;

            tween.frequency = frequency;
            tween.tweenType = TweenType.TrigRotate;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTrignometroc();
            return tween;
        }
        public static TweenDetail TrignometricScale(GameObject gameObject, Vector3 scaleLimit, Vector3 frequency)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localScale;
            tween.to = scaleLimit;
            tween.frequency = frequency;
            tween.tweenType = TweenType.TrigScale;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTrignometroc();
            return tween;
        }
        public static TweenDetail TrignometricMove(GameObject gameObject, Vector3 moveLimit, Vector3 frequency)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localPosition;
            tween.to = moveLimit;
            tween.frequency = frequency;
            tween.tweenType = TweenType.TrigMove;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTrignometroc();
            return tween;
        }
        public static TweenDetail TrignometricMoveTransform(GameObject gameObject, Transform from, Transform to, float frequency, float time, bool isContinuous = false)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.transformFrom = from;
            tween.transformTo = to;
            tween.frequency = new Vector3(frequency, 0, 0);
            tween.time = time;
            tween.isLooping = isContinuous;
            tween.tweenType = TweenType.TrigMoveTransform;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTrignometroc();
            return tween;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="frequency"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static TweenDetail TrignometricValue(GameObject gameObject, float start, float end, float frequency)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = new Vector3(start, 0, 0);
            tween.to = new Vector3(end, 0, 0);
            tween.frequency = new Vector3(frequency, 0, 0);
            tween.tweenType = TweenType.TrigSinValue;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTrignometroc();
            return tween;
        }
        public static TweenDetail RotateTo(GameObject gameObject, Vector3 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Rotate;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail ScaleTo(GameObject gameObject, Vector3 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localScale;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Scale;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail ScaleFrom(GameObject gameObject, Vector3 from, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = from;
            tween.to = gameObject.transform.localScale;
            tween.time = time;
            tween.tweenType = TweenType.Scale;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail PunchScale(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localScale;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.PunchScale;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail PunchPosition(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.PunchPosition;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail PunchPositionLocal(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localPosition;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.PunchPositionLocal;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail MoveAdd(GameObject gameObject, Vector3 amount, float time)
        {
            return MoveTo(gameObject, gameObject.transform.position + amount, time);
        }

        public static TweenDetail MoveBy(GameObject gameObject, Vector3 amount, float time)
        {
            return MoveTo(gameObject, gameObject.transform.position + amount, time);
        }

        public static TweenDetail ScaleAdd(GameObject gameObject, Vector3 amount, float time)
        {
            return ScaleTo(gameObject, gameObject.transform.localScale + amount, time);
        }

        public static TweenDetail ScaleBy(GameObject gameObject, Vector3 amount, float time)
        {
            Vector3 targetScale = new Vector3(
                gameObject.transform.localScale.x * amount.x,
                gameObject.transform.localScale.y * amount.y,
                gameObject.transform.localScale.z * amount.z
            );
            return ScaleTo(gameObject, targetScale, time);
        }

        public static TweenDetail RotateAdd(GameObject gameObject, Vector3 amount, float time)
        {
            return RotateTo(gameObject, gameObject.transform.eulerAngles + amount, time);
        }
        
        public static TweenDetail RotateFrom(GameObject gameObject, Vector3 from, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = from;
            tween.to = gameObject.transform.eulerAngles;
            tween.time = time;
            tween.tweenType = TweenType.Rotate;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail ShakePosition(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.position;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.ShakePosition;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail ShakeScale(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localScale;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.ShakeScale;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail ShakeRotation(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.ShakeRotation;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail FadeTo(GameObject gameObject, float alpha, float time)
        {
            return FadeObject(gameObject, alpha, time);
        }

        public static TweenDetail FadeFrom(GameObject gameObject, float alpha, float time)
        {
            TweenDetail tween = FadeObject(gameObject, 1f, time);
            if (tween != null)
            {
                tween.from = new Vector3(alpha, 0, 0);
            }
            return tween;
        }

        public static TweenDetail ColorTo(GameObject gameObject, Color color, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.colorTo = color;
            
            if (gameObject.TryGetComponent(out SpriteRenderer sr))
            {
                tween.spriteRenderer = sr;
                tween.colorFrom = sr.color;
            }
            else if (gameObject.TryGetComponent(out Renderer r))
            {
                tween.material = r.material;
                if (tween.material.HasProperty("_Color")) tween.colorFrom = tween.material.GetColor("_Color");
                else if (tween.material.HasProperty("_BaseColor")) tween.colorFrom = tween.material.GetColor("_BaseColor");
            }
            else if (gameObject.TryGetComponent(out Image img))
            {
                tween.colorFrom = img.color;
                // Reuse extraAdded to signify Image component
                tween.extraAdded = true; 
            }

            tween.time = time;
            tween.tweenType = TweenType.Color;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail ColorFrom(GameObject gameObject, Color color, float time)
        {
            TweenDetail tween = ColorTo(gameObject, color, time);
            if (tween != null)
            {
                // Swap
                Color temp = tween.colorTo;
                tween.colorTo = tween.colorFrom;
                tween.colorFrom = temp;
            }
            return tween;
        }

        public static TweenDetail AudioTo(GameObject gameObject, float volume, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            if (gameObject.TryGetComponent(out AudioSource source))
            {
                tween.audioSource = source;
                tween.floatFrom = source.volume;
                tween.floatTo = volume;
                tween.time = time;
                tween.tweenType = TweenType.Audio;
                tween.techTween = gameObject.AddComponent<TechTween>();
                tween.techTween.tweenDetail = tween;
                tween.StartTween();
                return tween;
            }
            return null;
        }

        public static TweenDetail AudioFrom(GameObject gameObject, float volume, float time)
        {
            TweenDetail tween = AudioTo(gameObject, volume, time);
            if (tween != null)
            {
                float temp = tween.floatTo;
                tween.floatTo = tween.floatFrom;
                tween.floatFrom = temp;
            }
            return tween;
        }

        public static TweenDetail LookTo(GameObject gameObject, Transform target, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            tween.lookTarget = target;
            tween.time = time;
            tween.tweenType = TweenType.Look;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        
        public static TweenDetail LookTo(GameObject gameObject, Vector3 targetPos, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            Vector3 direction = targetPos - gameObject.transform.position;
            tween.to = Quaternion.LookRotation(direction).eulerAngles;
            tween.time = time;
            tween.tweenType = TweenType.Rotate;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail LookFrom(GameObject gameObject, Transform target, float time)
        {
            Vector3 dir = target.position - gameObject.transform.position;
            Vector3 targetRot = Quaternion.LookRotation(dir).eulerAngles;
            return RotateFrom(gameObject, targetRot, time);
        }

        public static TweenDetail PunchRotation(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.eulerAngles;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.PunchRotation;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail PunchRotationLocal(GameObject gameObject, Vector3 amount, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = gameObject.transform.localEulerAngles;
            tween.to = amount;
            tween.time = time;
            tween.tweenType = TweenType.PunchRotationLocal;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail ValueTo(GameObject gameObject, float start, float to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = new Vector3(start, 0, 0);
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.Value;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail ValueTo(GameObject gameObject, int start, int to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = new Vector3(start, 0, 0);
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.Value;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

        public static TweenDetail ValueTo(MonoBehaviour mono, Vector2 start, Vector2 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = mono.transform;
            tween.from = start;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Value;
            tween.techTween = mono.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail ValueTo(MonoBehaviour mono, Vector3 start, Vector3 to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = mono.transform;
            tween.from = start;
            tween.to = to;
            tween.time = time;
            tween.tweenType = TweenType.Value;
            tween.techTween = mono.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static void SetPosition(RectTransform rect, Vector2 position)
        {
            rect.anchoredPosition = position;
        }
        public static TweenDetail MoveFrom(RectTransform rect, Vector3 from, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.rectTrans = rect;
            tween.from = from;
            tween.to = rect.position;
            tween.time = time;
            tween.tweenType = TweenType.Move;
            tween.techTween = rect.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail MoveFrom(GameObject gameObject, Vector3 from, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = from;
            tween.to = gameObject.transform.position;
            tween.time = time;
            tween.tweenType = TweenType.Move;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail CanvasAlpha(CanvasGroup group, float to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.canvasGroup = group;
            tween.from = new Vector3(group.alpha, 0, 0);
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.CanvasGroupAlpha;
            tween.techTween = group.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail SpriteRendererAlpha(SpriteRenderer sprite, float to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.spriteRenderer = sprite;
            tween.from = new Vector3(sprite.color.a, 0, 0);
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.SpriteRendererAlpha;
            tween.techTween = sprite.gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }
        public static TweenDetail FaceUI(GameObject gameObject, int start, int to, float time)
        {
            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            tween.from = new Vector3(start, 0, 0);
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.FadeUi;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            if (gameObject.TryGetComponent(out CanvasGroup canvas))
            {
                tween.canvasGroup = canvas;
            }
            else
            {
                tween.extraAdded = true;
                tween.canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            tween.StartTween();
            return tween;
        }

        public static TweenDetail FadeSprite(GameObject gameObject, float to, float time)
        {
            if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                return SpriteRendererAlpha(spriteRenderer, to, time);
            }
            return null;
        }

        public static TweenDetail FadeObject(GameObject gameObject, float to, float time)
        {
            if (gameObject == null) return null;
            if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                return SpriteRendererAlpha(spriteRenderer, to, time);
            }

            TweenDetail tween = new TweenDetail();
            tween.reset();
            tween.trans = gameObject.transform;
            if (gameObject.TryGetComponent(out Renderer renderer))
            {
                tween.material = renderer.material;
                if (tween.material != null)
                {
                    float startAlpha = 1f;
                    if (tween.material.HasProperty("_Color"))
                        startAlpha = tween.material.GetColor("_Color").a;
                    else if (tween.material.HasProperty("_BaseColor"))
                        startAlpha = tween.material.GetColor("_BaseColor").a;

                    tween.from = new Vector3(startAlpha, 0, 0);

                    // Ensure standard shader is set to fade/transparent
                    if (tween.material.HasProperty("_Mode") && tween.material.GetFloat("_Mode") == 0)
                    {
                        tween.material.SetFloat("_Mode", 2);
                        tween.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        tween.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        tween.material.SetInt("_ZWrite", 0);
                        tween.material.DisableKeyword("_ALPHATEST_ON");
                        tween.material.EnableKeyword("_ALPHABLEND_ON");
                        tween.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        tween.material.renderQueue = 3000;
                    }
                    // Ensure URP Lit is set to transparent
                    if (tween.material.HasProperty("_Surface") && tween.material.GetFloat("_Surface") == 0)
                    {
                        tween.material.SetFloat("_Surface", 1);
                        tween.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        tween.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        tween.material.SetInt("_ZWrite", 0);
                        tween.material.renderQueue = 3000;
                    }
                }
            }
            tween.to = new Vector3(to, 0, 0);
            tween.time = time;
            tween.tweenType = TweenType.MaterialAlpha;
            tween.techTween = gameObject.AddComponent<TechTween>();
            tween.techTween.tweenDetail = tween;
            tween.StartTween();
            return tween;
        }

    }
    [Serializable]
    public class TweenDetail
    {
        public bool isLocal = false;
        public bool isJumping = false;
        public bool isLooping = false;
        public bool isRunning = false;
        public bool isPingPong = false;
        public bool isReversing = false;

        public float time;
        public float delay;
        public float speed;
        public float startTime;

        public bool extraAdded = false;

        public int repeat = 1;
        public int loopCount;

        public Vector3 to;
        public Vector3 from;
        public Vector3 axis;
        public Vector3 newVect;
        public Vector3 frequency;
        public Vector3 arcHeight;
        public List<Vector3> pathPoints;

        public Transform trans;
        public Transform transformFrom;
        public Transform transformTo;

        public TweenType tweenType;
        public EaseTween easeTween = EaseTween.Linear;
        public OperationType operationType = OperationType.None;

        public Action action;
        public TechTween techTween;
        public SpriteRenderer spriteRenderer;
        public CanvasGroup canvasGroup;
        public RectTransform rectTrans;
        public Material material;
        public AnimationCurve animationCurve;
        public TweenUpdate tweenUpdates = new TweenUpdate();
        public float pauseTime;
        public Color colorFrom;
        public Color colorTo;
        public AudioSource audioSource;
        public float floatFrom;
        public float floatTo;
        public Transform lookTarget;

        public TweenDetail() { }
        public void CancleTween()
        {
            GameObject.Destroy(techTween);
        }
        public void Pause()
        {
            pauseTime = Time.time;
        }
        public void Resume()
        {
            float timePaused = Time.time - pauseTime;
            startTime += timePaused;
        }
        public void reset()
        {
            isLooping = false;
            repeat = 1;
            trans = null;
            delay = 0.0f;
            from = to = Vector3.zero;
            isPingPong = false;
            isReversing = false;
            tweenUpdates.Reset();
        }
        public void StartAction()
        {
            time = 0f;
            operationType = OperationType.CallInSec;
        }
        public void StartJump()
        {
            startTime = Time.time + delay;
            loopCount = 0;
            tweenUpdates.OnTweenStart?.Invoke();
            isJumping = true;
        }
        public void StartTween()
        {
            startTime = Time.time + delay;
            loopCount = 0;
            tweenUpdates.OnTweenStart?.Invoke();
            isRunning = true;
        }
        public void ContinueUpdateTween()
        {
            if (!isRunning) return;
            switch (tweenType)
            {
                case TweenType.KeepRotate:
                    if (trans != null)
                    {
                        trans.Rotate(axis, speed * Time.deltaTime, Space.World);
                    }
                    break;
                case TweenType.KeepRotateLocal:
                    if (trans != null)
                    {
                        trans.Rotate(axis, speed * Time.deltaTime, Space.Self);
                    }
                    break;
            }
        }
        public float progress;
        public void UpdateTween()
        {
            switch (operationType)
            {
                case OperationType.CallInSec:
                    time += Time.deltaTime;
                    if (time >= (1f / (float)repeat))
                    {
                        time -= (1f / (float)repeat);
                        action?.Invoke();
                    }
                    break;
            }
            if (isJumping)
            {
                float elapsedTime = Time.time - startTime;
                if (elapsedTime < 0) return;
                if (elapsedTime < time)
                {
                    progress = elapsedTime / time;
                    float easedProgress = GetValue(progress);
                    float parabola = 1.0f - 4.0f * (easedProgress - 0.5f) * (easedProgress - 0.5f);
                    Vector3 nextPos = Vector3.Lerp(from, to, easedProgress);
                    nextPos.x += parabola * arcHeight.x;
                    nextPos.y += parabola * arcHeight.y;
                    nextPos.z += parabola * arcHeight.z;
                    
                    if (trans != null)
                    {
                        if(isLocal)
                        {
                            trans.localPosition = nextPos;
                        }
                        else
                        {
                            trans.position = nextPos;
                        }
                           
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.anchoredPosition = nextPos;
                    }
                }
                else
                {
                    if (trans != null)
                    {
                        if (isLocal)
                        {
                            trans.localPosition = to;
                        }
                        else
                        {
                            trans.position = to;
                        }
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.anchoredPosition = to;
                    }
                    progress = 1f;
                    tweenUpdates.OnTweenComplete?.Invoke();
                    isJumping = false;
                    if (techTween != null) GameObject.Destroy(techTween);
                }
            }
            if (isRunning)
            {
                float elapsedTime = Time.time - startTime;
                if (elapsedTime < 0) return; // Waiting for delay
                if (elapsedTime < time)
                {
                    progress = elapsedTime / time;
                    SetValues(GetValue(progress),false);
                }
                else
                {
                    progress = 1f;
                    SetValues(1,true);
                    action?.Invoke();
                    tweenUpdates.OnTweenComplete?.Invoke();
                    loopCount++;
                    if (isPingPong)
                    {
                        isReversing = !isReversing;
                    }
                    if (isLooping || loopCount < repeat)
                    {
                        startTime = Time.time; // Restart loop
                    }
                    else
                    {
                        isRunning = false;
                        if (techTween != null) GameObject.Destroy(techTween);
                    }
                }
            }
        }
        private Vector3 startValue;
        public void StartTrignometroc()
        {
            startTime = Time.time + delay;
            switch (tweenType)
            {
                case TweenType.TrigRotate:
                    if (trans != null)
                    {
                        startValue = trans.localEulerAngles;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localEulerAngles;
                    }
                    break;
                case TweenType.TrigScale:
                    if (trans != null)
                    {
                        startValue = trans.localScale;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localScale;
                    }
                    break;
                case TweenType.TrigMove:
                    if (trans != null)
                    {
                        startValue = trans.localPosition;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localPosition;
                    }
                    break;
                case TweenType.TrigSinValue:
                    startValue = from;
                    break;
                case TweenType.TrigMoveTransform:
                    // Does not require a start value since we lerp between transforms
                    break;
            }
            isRunning = true;
        }
        public void UpdateTrignometric()
        {

            if (!isRunning) return;
            float elapsedTime = Time.time - startTime;
            if (elapsedTime < 0) return; // Waiting for delay

            if (tweenType == TweenType.TrigMoveTransform)
            {
                if (transformFrom != null && transformTo != null)
                {
                    float progress = time > 0 ? elapsedTime / time : elapsedTime;
                    float t = (1f - Mathf.Cos(progress * frequency.x * 2f * Mathf.PI)) / 2f;
                    Vector3 newPos = Vector3.Lerp(transformFrom.position, transformTo.position, t);
                    tweenUpdates.onUpdateVector3?.Invoke(newPos);
                    if (trans != null)
                    {
                        trans.position = newPos;
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.position = newPos;
                    }

                    if (time > 0 && elapsedTime >= time && !isLooping)
                    {
                        tweenUpdates.OnTweenComplete?.Invoke();
                        isRunning = false;
                        if (techTween != null) GameObject.Destroy(techTween);
                    }
                }
                return;
            }

            if (to == Vector3.zero || frequency == Vector3.zero) return;
            // Compute oscillating values
            float x = startValue.x + Mathf.Sin(Time.timeSinceLevelLoad * frequency.x) * to.x;
            float y = startValue.y + Mathf.Sin(Time.timeSinceLevelLoad * frequency.y) * to.y;
            float z = startValue.z + Mathf.Sin(Time.timeSinceLevelLoad * frequency.z) * to.z;
            tweenUpdates.onUpdateValue?.Invoke(x);
            // Apply transformation based on type
            switch (tweenType)
            {
                case TweenType.TrigRotate:
                    if (trans != null)
                    {
                        trans.transform.eulerAngles = new Vector3(x, y, z);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.eulerAngles = new Vector3(x, y, z);
                    }
                    break;
                case TweenType.TrigScale:
                    if (trans != null)
                    {
                        trans.transform.localScale = new Vector3(x, y, z);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.localScale = new Vector3(x, y, z);
                    }
                    break;
                case TweenType.TrigMove:
                    if (trans != null)
                    {
                        trans.transform.localPosition = new Vector3(x, y, z);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.localPosition = new Vector3(x, y, z);
                    }
                    break;

            }
        }
        public IEnumerator RunTween()
        {
            yield return new WaitForSeconds(delay);
            int loops = isLooping ? int.MaxValue : repeat;
            for (int i = 0; i < loops; i++)
            {
                tweenUpdates.OnTweenStart?.Invoke();
                float elapsedTime = 0;
                while (elapsedTime < time)
                {
                    if (!TechTween.IsPaused)
                    {
                        elapsedTime += Time.deltaTime;
                        SetValues(GetValue(elapsedTime / time),false);
                    }
                    yield return null;
                }
                SetValues(1,true);
                tweenUpdates.OnTweenComplete?.Invoke();
                if (isPingPong)
                {
                    isReversing = !isReversing; // Toggle direction after each cycle
                }
            }
            if (techTween != null)
            {
                GameObject.Destroy(techTween);
            }
        }
        public IEnumerator TrignometricObject()
        {
            Vector3 startValue = Vector3.zero;
            switch (tweenType)
            {
                case TweenType.TrigRotate:
                    if (trans != null)
                    {
                        startValue = trans.localEulerAngles;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localEulerAngles;
                    }
                    break;
                case TweenType.TrigScale:
                    if (trans != null)
                    {
                        startValue = trans.localScale;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localScale;
                    }
                    break;
                case TweenType.TrigMove:
                    if (trans != null)
                    {
                        startValue = trans.localPosition;
                    }
                    if (rectTrans != null)
                    {
                        startValue = rectTrans.localPosition;
                    }
                    break;
                case TweenType.TrigMoveTransform:
                    break;

            }
            float coroutineStartTime = Time.time;
            while (true) // Keep rotating indefinitely
            {
                if (TechTween.IsPaused)
                {
                    coroutineStartTime += Time.deltaTime; // Shift start time to pause
                    yield return null;
                    continue;
                }
                float elapsedTime = Time.time - coroutineStartTime;
                if (tweenType == TweenType.TrigMoveTransform)
                {
                    if (transformFrom != null && transformTo != null)
                    {
                        float progress = time > 0 ? elapsedTime / time : elapsedTime;
                        float t = (1f - Mathf.Cos(progress * frequency.x * 2f * Mathf.PI)) / 2f;
                        Vector3 newPos = Vector3.Lerp(transformFrom.position, transformTo.position, t);
                        if (trans != null)
                        {
                            trans.position = newPos;
                        }
                        if (rectTrans != null)
                        {
                            rectTrans.position = newPos;
                        }

                        if (time > 0 && elapsedTime >= time && !isLooping)
                        {
                            tweenUpdates.OnTweenComplete?.Invoke();
                            break;
                        }
                    }
                }
                else if (to != Vector3.zero && frequency != Vector3.zero)
                {
                    // Calculate the rotation for each axis based on sine wave and time
                    float x = startValue.x + Mathf.Sin(Time.timeSinceLevelLoad * frequency.x) * to.x;
                    float y = startValue.y + Mathf.Sin(Time.timeSinceLevelLoad * frequency.y) * to.y;
                    float z = startValue.z + Mathf.Sin(Time.timeSinceLevelLoad * frequency.z) * to.z;
                    // Apply the final calculated rotation
                    switch (tweenType)
                    {
                        case TweenType.TrigRotate:
                            if (trans != null)
                            {
                                trans.transform.eulerAngles = new Vector3(x, y, z);
                            }
                            if (rectTrans != null)
                            {
                                rectTrans.transform.eulerAngles = new Vector3(x, y, z);
                            }
                            break;
                        case TweenType.TrigScale:
                            if (trans != null)
                            {
                                trans.transform.localScale = new Vector3(x, y, z);
                            }
                            if (rectTrans != null)
                            {
                                rectTrans.transform.localScale = new Vector3(x, y, z);
                            }
                            break;
                        case TweenType.TrigMove:
                            if (trans != null)
                            {
                                trans.transform.localPosition = new Vector3(x, y, z);
                            }
                            if (rectTrans != null)
                            {
                                rectTrans.transform.localPosition = new Vector3(x, y, z);
                            }
                            break;

                    }
                }
                // Wait for the next frame before continuing
                yield return null;
            }
        }

        public void SetValues(float valu,bool isDone)
        {
            // Determine the direction of the tween based on ping-pong
            Vector3 start = isReversing ? to : from;
            Vector3 end = isReversing ? from : to;

            newVect = Vector3.Lerp(start, end, valu);
            tweenUpdates.onUpdateIntValue?.Invoke(Mathf.RoundToInt(newVect.x));
            tweenUpdates.onUpdateValue?.Invoke(newVect.x);
            tweenUpdates.onUpdateVector2?.Invoke(new Vector2(newVect.x, newVect.y));
            tweenUpdates.onUpdateVector3?.Invoke(newVect);
            switch (tweenType)
            {
                case TweenType.Value:
                    break;
                case TweenType.FadeUi:
                    if (canvasGroup != null)
                    {
                        canvasGroup.alpha = valu;
                        if (isDone && extraAdded)
                        {
                            MonoBehaviour.Destroy(canvasGroup);
                        }
                    }
                    break;
                case TweenType.MoveLocal:
                    if (trans != null)
                    {
                        trans.transform.localPosition = Vector3.Lerp(start, end, valu);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.localPosition = Vector3.Lerp(start, end, valu);
                    }
                    break;
                case TweenType.Move:
                    if (trans != null)
                    {
                        trans.transform.position = Vector3.Lerp(start, end, valu);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.position = Vector3.Lerp(start, end, valu);
                    }
                    break;
                case TweenType.MoveOnPoints:
                    if (pathPoints != null && pathPoints.Count > 0)
                    {
                        int segments = pathPoints.Count;
                        float totalProgress = valu * segments;
                        int currentIndex = Mathf.FloorToInt(totalProgress);
                        if (currentIndex >= segments) 
                        {
                            currentIndex = segments - 1;
                            totalProgress = segments;
                        }
                        float segmentProgress = totalProgress - currentIndex;
                        Vector3 startPos = currentIndex == 0 ? from : pathPoints[currentIndex - 1];
                        Vector3 endPos = pathPoints[currentIndex];
                        
                        Vector3 nextPos = Vector3.Lerp(startPos, endPos, segmentProgress);
                        
                        if (trans != null)
                        {
                            if (isLocal) trans.localPosition = nextPos;
                            else trans.position = nextPos;
                        }
                        if (rectTrans != null)
                        {
                            if (isLocal) rectTrans.localPosition = nextPos;
                            else rectTrans.position = nextPos;
                        }
                    }
                    break;
                case TweenType.Scale:
               
                    if (trans != null)
                    {
                        trans.transform.localScale = Vector3.Lerp(start, end, valu);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.localScale = Vector3.Lerp(start, end, valu);
                    }
                    break;
                case TweenType.PunchScale:
                    Vector3 pScale = new Vector3(
                        GetPunch(end.x, valu),
                        GetPunch(end.y, valu),
                        GetPunch(end.z, valu)
                    );
                    if (trans != null) trans.transform.localScale = start + pScale;
                    if (rectTrans != null) rectTrans.transform.localScale = start + pScale;
                    break;
                case TweenType.PunchPosition:
                    Vector3 pPos = new Vector3(
                        GetPunch(end.x, valu),
                        GetPunch(end.y, valu),
                        GetPunch(end.z, valu)
                    );
                    if (trans != null) trans.transform.position = start + pPos;
                    if (rectTrans != null) rectTrans.transform.position = start + pPos;
                    break;
                case TweenType.PunchPositionLocal:
                    Vector3 pPosL = new Vector3(
                        GetPunch(end.x, valu),
                        GetPunch(end.y, valu),
                        GetPunch(end.z, valu)
                    );
                    if (trans != null) trans.transform.localPosition = start + pPosL;
                    if (rectTrans != null) rectTrans.transform.localPosition = start + pPosL;
                    break;
                case TweenType.PunchRotation:
                    Vector3 pRot = new Vector3(
                        GetPunch(end.x, valu),
                        GetPunch(end.y, valu),
                        GetPunch(end.z, valu)
                    );
                    if (trans != null) trans.transform.eulerAngles = start + pRot;
                    if (rectTrans != null) rectTrans.transform.eulerAngles = start + pRot;
                    break;
                case TweenType.PunchRotationLocal:
                    Vector3 pRotL = new Vector3(
                        GetPunch(end.x, valu),
                        GetPunch(end.y, valu),
                        GetPunch(end.z, valu)
                    );
                    if (trans != null) trans.transform.localEulerAngles = start + pRotL;
                    if (rectTrans != null) rectTrans.transform.localEulerAngles = start + pRotL;
                    break;
                case TweenType.ShakePosition:
                    if (trans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        trans.position = start + shake;
                    }
                    if (rectTrans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        rectTrans.position = start + shake;
                    }
                    break;
                case TweenType.ShakeScale:
                    if (trans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        trans.localScale = start + shake;
                    }
                    if (rectTrans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        rectTrans.localScale = start + shake;
                    }
                    break;
                case TweenType.ShakeRotation:
                    if (trans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        trans.eulerAngles = start + shake;
                    }
                    if (rectTrans != null)
                    {
                        Vector3 shake = new Vector3(
                            UnityEngine.Random.Range(-end.x, end.x),
                            UnityEngine.Random.Range(-end.y, end.y),
                            UnityEngine.Random.Range(-end.z, end.z)
                        ) * (1f - valu);
                        rectTrans.eulerAngles = start + shake;
                    }
                    break;
                case TweenType.Color:
                    Color currColor = Color.Lerp(colorFrom, colorTo, valu);
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = currColor;
                    }
                    else if (material != null)
                    {
                        if (material.HasProperty("_Color")) material.SetColor("_Color", currColor);
                        else if (material.HasProperty("_BaseColor")) material.SetColor("_BaseColor", currColor);
                    }
                    else if (extraAdded) 
                    {
                        if (trans.TryGetComponent(out Image img)) img.color = currColor;
                    }
                    break;
                case TweenType.Audio:
                    if (audioSource != null)
                    {
                        audioSource.volume = Mathf.Lerp(floatFrom, floatTo, valu);
                    }
                    break;
                case TweenType.Look:
                    if (trans != null && lookTarget != null)
                    {
                        Vector3 dir = lookTarget.position - trans.position;
                        if (dir != Vector3.zero)
                        {
                            Quaternion targetRot = Quaternion.LookRotation(dir);
                            trans.rotation = Quaternion.Lerp(Quaternion.Euler(start), targetRot, valu);
                        }
                    }
                    break;
                case TweenType.Rotate:
                    float xNextRot = closestRot(start.x, end.x);
                    float yNextRot = closestRot(start.y, end.y);
                    float zNextRot = closestRot(start.z, end.z);
                    if (trans != null)
                    {
                        trans.transform.eulerAngles = Vector3.Lerp(start, new Vector3(xNextRot, yNextRot, zNextRot), valu);
                    }
                    if (rectTrans != null)
                    {
                        rectTrans.transform.localEulerAngles = Vector3.Lerp(start, new Vector3(xNextRot, yNextRot, zNextRot), valu);
                    }
                    break;
                case TweenType.CanvasGroupAlpha:
                    if (canvasGroup != null)
                    {
                        canvasGroup.alpha = newVect.x;
                    }
                    break;
                case TweenType.SpriteRendererAlpha:
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newVect.x);
                    }
                    break;
                case TweenType.MaterialAlpha:
                    if (material != null)
                    {
                        if (material.HasProperty("_Color"))
                        {
                            Color c = material.GetColor("_Color");
                            c.a = newVect.x;
                            material.SetColor("_Color", c);
                        }
                        if (material.HasProperty("_BaseColor"))
                        {
                            Color c = material.GetColor("_BaseColor");
                            c.a = newVect.x;
                            material.SetColor("_BaseColor", c);
                        }
                    }
                    break;
            }
        }

        private float GetPunch(float amplitude, float value)
        {
            if (value == 0 || value == 1) return 0;
            float period = 0.3f;
            return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin(value * 2 * Mathf.PI / period));
        }

        //Generated from Ai Tools
        public float GetValue(float t)
        {
            if (animationCurve != null)
            {
                return animationCurve.Evaluate(t);
            }
            float value = t;
            switch (easeTween)
            {
                case EaseTween.Linear:
                    value = t;
                    break;
                case EaseTween.EaseInQuad:
                    value = t * t;
                    break;
                case EaseTween.EaseOutQuad:
                    value = -t * (t - 2);
                    break;
                case EaseTween.EaseInOutQuad:
                    value = t < 0.5f ? 2 * t * t : -2 * t * (t - 2) - 1;
                    break;
                case EaseTween.EaseOutInQuad:
                    value = t < 0.5f ? 0.5f * (-2 * t * (t - 1)) : 0.5f * ((2 * t - 1) * (2 * t - 1)) + 0.25f;
                    break;

                case EaseTween.EaseInCubic:
                    value = t * t * t;
                    break;
                case EaseTween.EaseOutCubic:
                    float f = t - 1;
                    value = f * f * f + 1;
                    break;
                case EaseTween.EaseInOutCubic:
                    value = t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
                    break;
                case EaseTween.EaseOutInCubic:
                    value = t < 0.5f
                        ? 0.5f * ((2 * t - 1) * (2 * t - 1) * (2 * t - 1) + 1)
                        : 0.5f * (2 * t - 1) * (2 * t - 1) * (2 * t - 1) + 0.5f;
                    break;

                case EaseTween.EaseInQuart:
                    value = t * t * t * t;
                    break;
                case EaseTween.EaseOutQuart:
                    f = t - 1;
                    value = 1 - f * f * f * f;
                    break;
                case EaseTween.EaseInOutQuart:
                    value = t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (t - 1) * (t - 1) * (t - 1) * (t - 1);
                    break;
                case EaseTween.EaseOutInQuart:
                    value = t < 0.5f
                        ? 0.5f * (1 - Mathf.Pow(1 - 2 * t, 4))
                        : 0.5f * Mathf.Pow(2 * t - 1, 4) + 0.5f;
                    break;

                case EaseTween.EaseInQuint:
                    value = t * t * t * t * t;
                    break;
                case EaseTween.EaseOutQuint:
                    f = t - 1;
                    value = f * f * f * f * f + 1;
                    break;
                case EaseTween.EaseInOutQuint:
                    value = t < 0.5f ? 16 * t * t * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) * (2 * t - 2) * (2 * t - 2) + 1;
                    break;
                case EaseTween.EaseOutInQuint:
                    value = t < 0.5f
                        ? 0.5f * (Mathf.Pow(2 * t - 1, 5) + 1)
                        : 0.5f * Mathf.Pow(2 * t - 1, 5) + 0.5f;
                    break;

                case EaseTween.EaseInSine:
                    value = 1 - Mathf.Cos((t * Mathf.PI) / 2);
                    break;
                case EaseTween.EaseOutSine:
                    value = Mathf.Sin((t * Mathf.PI) / 2);
                    break;
                case EaseTween.EaseInOutSine:
                    value = 0.5f * (1 - Mathf.Cos(Mathf.PI * t));
                    break;
                case EaseTween.EaseOutInSine:
                    value = t < 0.5f
                        ? 0.5f * Mathf.Sin(t * 2 * Mathf.PI / 2)
                        : 0.5f * (1 - Mathf.Cos((2 * t - 1) * Mathf.PI / 2)) + 0.5f;
                    break;

                case EaseTween.EaseInExpo:
                    value = (t == 0) ? 0 : Mathf.Pow(2, 10 * (t - 1));
                    break;
                case EaseTween.EaseOutExpo:
                    value = (t == 1) ? 1 : 1 - Mathf.Pow(2, -10 * t);
                    break;
                case EaseTween.EaseInOutExpo:
                    if (t == 0 || t == 1) value = t;
                    else value = t < 0.5f ? Mathf.Pow(2, 10 * (2 * t - 1)) / 2 : (2 - Mathf.Pow(2, -10 * (2 * t - 1))) / 2;
                    break;
                case EaseTween.EaseOutInExpo:
                    value = t < 0.5f
                        ? 0.5f * (1 - Mathf.Pow(2, -20 * t))
                        : 0.5f * Mathf.Pow(2, 20 * (t - 1)) + 0.5f;
                    break;

                case EaseTween.EaseInCirc:
                    value = 1 - Mathf.Sqrt(1 - t * t);
                    break;
                case EaseTween.EaseOutCirc:
                    value = Mathf.Sqrt(1 - (t - 1) * (t - 1));
                    break;
                case EaseTween.EaseInOutCirc:
                    value = t < 0.5f ? (1 - Mathf.Sqrt(1 - 4 * t * t)) / 2 : (Mathf.Sqrt(1 - (2 - 2 * t) * (2 - 2 * t)) + 1) / 2;
                    break;
                case EaseTween.EaseOutInCirc:
                    value = t < 0.5f
                        ? 0.5f * Mathf.Sqrt(1 - (2 * t - 1) * (2 * t - 1))
                        : 0.5f * (1 - Mathf.Sqrt(1 - (2 * t - 1) * (2 * t - 1))) + 0.5f;
                    break;

                case EaseTween.EaseInElastic:
                    value = Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t - 1.1f) * 5 * Mathf.PI);
                    break;
                case EaseTween.EaseOutElastic:
                    value = Mathf.Pow(2, -10 * t) * Mathf.Sin((t - 0.1f) * 5 * Mathf.PI) + 1;
                    break;
                case EaseTween.EaseInOutElastic:
                    value = t < 0.5f
                        ? 0.5f * Mathf.Pow(2, 10 * (2 * t - 1)) * Mathf.Sin((2 * t - 1.1f) * 5 * Mathf.PI)
                        : 0.5f * Mathf.Pow(2, -10 * (2 * t - 1)) * Mathf.Sin((2 * t - 1.1f) * 5 * Mathf.PI) + 1;
                    break;
                case EaseTween.EaseOutInElastic:
                    value = t < 0.5f
                        ? 0.5f * (Mathf.Pow(2, -10 * (2 * t)) * Mathf.Sin((2 * t - 0.1f) * 5 * Mathf.PI)) + 0.5f
                        : 0.5f * Mathf.Pow(2, 10 * (2 * t - 2)) * Mathf.Sin((2 * t - 1.1f) * 5 * Mathf.PI);
                    break;

                case EaseTween.EaseInBack:
                    float s = 1.70158f;
                    value = t * t * ((s + 1) * t - s);
                    break;
                case EaseTween.EaseOutBack:
                    s = 1.70158f;
                    f = t - 1;
                    value = 1 + f * f * ((s + 1) * f + s);
                    break;
                case EaseTween.EaseInOutBack:
                    s = 1.70158f;
                    value = t < 0.5f
                        ? 0.5f * (t * t * ((s * 1.525f + 1) * t - s * 1.525f))
                        : 0.5f * ((t - 1) * (t - 1) * ((s * 1.525f + 1) * (t - 1) + s * 1.525f) + 2);
                    break;
                case EaseTween.EaseOutInBack:
                    s = 1.70158f;
                    if (t < 0.5f)
                    {
                        f = 2 * t - 1;
                        value = 0.5f * (1 + f * f * ((s + 1) * f + s));
                    }
                    else
                    {
                        f = 2 * t - 1;
                        value = 0.5f * (f * f * ((s + 1) * f - s)) + 0.5f;
                    }
                    break;

                case EaseTween.EaseOutBounce:
                    value = EaseOutBounce(t);
                    break;
                case EaseTween.EaseInBounce:
                    value = 1 - EaseOutBounce(1 - t);
                    break;
                case EaseTween.EaseInOutBounce:
                    value = t < 0.5f ? (1 - EaseOutBounce(1 - 2 * t)) * 0.5f : (1 + EaseOutBounce(2 * t - 1)) * 0.5f;
                    break;
                case EaseTween.EaseOutInBounce:
                    value = t < 0.5f
                        ? 0.5f * EaseOutBounce(2 * t)
                        : 0.5f * (1 - EaseOutBounce(2 - 2 * t)) + 0.5f;
                    break;

                case EaseTween.EaseSpring:
                    value = Mathf.Sin(t * Mathf.PI * (0.2f + 2.5f * t * t * t)) * Mathf.Pow(1f - t, 2.2f) + t;
                    break;
                case EaseTween.SmoothStep:
                    value = t * t * (3f - 2f * t);
                    break;
                case EaseTween.SmootherStep:
                    value = t * t * t * (t * (6f * t - 15f) + 10f);
                    break;
            }
            return value;
        }

        private float EaseOutBounce(float t)
        {
            if (t < (1 / 2.75f))
                return 7.5625f * t * t;
            else if (t < (2 / 2.75f))
                return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
            else if (t < (2.5 / 2.75f))
                return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
            else
                return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }
        public float closestRot(float from, float to)
        {
            float minusWhole = 0 - (360 - to);
            float plusWhole = 360 + to;
            float toDiffAbs = Mathf.Abs(to - from);
            float minusDiff = Mathf.Abs(minusWhole - from);
            float plusDiff = Mathf.Abs(plusWhole - from);
            if (toDiffAbs < minusDiff && toDiffAbs < plusDiff)
            {
                return to;
            }
            else
            {
                if (minusDiff < plusDiff)
                {
                    return minusWhole;
                }
                else
                {
                    return plusWhole;
                }
            }
        }
        public TweenDetail SetEaseType(EaseTween _easeTween)
        {
            easeTween = _easeTween;
            return this;
        }
        public TweenDetail SetDelay(float _delay)
        {
            float oldDelay = delay;
            delay = _delay;
            startTime += (delay - oldDelay);
            return this;
        }
        public TweenDetail SetLocal()
        {
            isLocal = true;
            if (isJumping)
            {
                if (trans != null && from == trans.position)
                {
                    from = trans.localPosition;
                }
                return this;
            }
            if (trans != null)
            {
                if ((tweenType == TweenType.Move || tweenType == TweenType.MoveOnPoints) && from == trans.position)
                {
                    if (tweenType == TweenType.Move) tweenType = TweenType.MoveLocal;
                    from = trans.localPosition;
                }
                else if (tweenType == TweenType.Rotate && from == trans.eulerAngles)
                {
                    tweenType = TweenType.RotateLocal;
                    from = trans.localEulerAngles;
                }
                else if (tweenType == TweenType.KeepRotate)
                {
                    tweenType = TweenType.KeepRotateLocal;
                }
            }
            if (rectTrans != null)
            {
                if ((tweenType == TweenType.Move || tweenType == TweenType.MoveOnPoints) && from == rectTrans.position)
                {
                    if (tweenType == TweenType.Move) tweenType = TweenType.MoveLocal;
                    from = rectTrans.localPosition;
                }
            }
            return this;
        }
        public TweenDetail SetOnTweenStart(Action onComplete)
        {
            tweenUpdates.OnTweenStart = onComplete;
            return this;
        }
        public TweenDetail GetCompleteCallback(Action onComplete)
        {
            tweenUpdates.OnTweenComplete = onComplete;
            return this;
        }
        public TweenDetail GetVector3Update(Action<Vector3> onUpdate)
        {
            tweenUpdates.onUpdateVector3 = onUpdate;
            return this;
        }
        public TweenDetail GetValueUpdate(Action<float> onUpdate)
        {
            tweenUpdates.onUpdateValue = onUpdate;
            return this;
        }
        public TweenDetail GetValueIntUpdate(Action<int> onUpdate)
        {
            tweenUpdates.onUpdateIntValue = onUpdate;
            return this;
        }
        public TweenDetail GetVector2Update(Action<Vector2> onUpdate)
        {
            tweenUpdates.onUpdateVector2 = onUpdate;
            return this;
        }
        public TweenDetail SetLoopCount(int count)
        {
            repeat = count;
            return this;
        }
        public TweenDetail SetInfiniteLoop(bool loop)
        {
            isLooping = loop;
            return this;
        }
        public TweenDetail SetPingPong(bool pingPong)
        {
            isPingPong = pingPong;
            return this;
        }
    }

    /// <summary>
    /// Handles moving individual particles within a ParticleSystem along random bezier curves to a target.
    /// </summary>
    public class ParticleBezierUpdater : MonoBehaviour
    {
        public ParticleSystem ps;
        public Vector3 targetPosition;
        public float randomness = 0.5f;

        private ParticleSystem.Particle[] particles;
        private Dictionary<uint, Vector3> particleStartPos = new Dictionary<uint, Vector3>();
        private Dictionary<uint, Vector3> particleControlPos = new Dictionary<uint, Vector3>();
        private List<uint> keysToRemove = new List<uint>();
        private HashSet<uint> aliveSeeds = new HashSet<uint>();

        void LateUpdate()
        {
            if (ps == null) return;
            
            int maxParticles = ps.main.maxParticles;
            if (particles == null || particles.Length < maxParticles)
                particles = new ParticleSystem.Particle[maxParticles];

            int numParticlesAlive = ps.GetParticles(particles);
            aliveSeeds.Clear();

            bool isLocal = ps.main.simulationSpace == ParticleSystemSimulationSpace.Local;
            Vector3 actualTarget = isLocal ? ps.transform.InverseTransformPoint(targetPosition) : targetPosition;

            for (int i = 0; i < numParticlesAlive; i++)
            {
                uint id = particles[i].randomSeed;
                aliveSeeds.Add(id);

                if (!particleStartPos.ContainsKey(id))
                {
                    particleStartPos[id] = particles[i].position;
                    
                    Vector3 midPoint = (particles[i].position + actualTarget) / 2f;
                    float distance = Vector3.Distance(particles[i].position, actualTarget);
                    
                    // Predictable randomness based on seed
                    UnityEngine.Random.InitState((int)id);
                    Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * (distance * randomness);
                    particleControlPos[id] = midPoint + randomOffset;
                }

                float progress = 1.0f - (particles[i].remainingLifetime / particles[i].startLifetime);
                
                Vector3 start = particleStartPos[id];
                Vector3 control = particleControlPos[id];
                
                float t = progress;
                // Quadratic bezier evaluation
                particles[i].position = (1 - t) * (1 - t) * start + 2 * (1 - t) * t * control + t * t * actualTarget;
            }

            ps.SetParticles(particles, numParticlesAlive);

            // Cleanup dictionary logic
            if (particleStartPos.Count > numParticlesAlive)
            {
                keysToRemove.Clear();
                foreach (var key in particleStartPos.Keys)
                {
                    if (!aliveSeeds.Contains(key)) keysToRemove.Add(key);
                }
                foreach (var key in keysToRemove)
                {
                    particleStartPos.Remove(key);
                    particleControlPos.Remove(key);
                }
            }
        }
    }
}


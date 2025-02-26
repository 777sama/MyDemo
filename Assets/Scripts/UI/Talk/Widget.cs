using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Widget : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private AnimationCurve fadingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public float RenderOpacity
        {
            get => canvasGroup.alpha;
            
            set => canvasGroup.alpha = value;
        }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private Coroutine fadeCoroutine;
        public void Fade(float opacity,float duration,Action OnFinished)
        {
            if (duration <= 0)
            {
                canvasGroup.alpha = opacity;
                OnFinished?.Invoke();
            }
            else
            {
                if (fadeCoroutine != null)
                {
                    
                    StopCoroutine(fadeCoroutine);
                }
               
                fadeCoroutine = StartCoroutine(Fading(opacity, duration, OnFinished));
            }
        }

        IEnumerator Fading(float opacity, float duration, Action OnFinished)
        {
            float timer = 0;
            float start = RenderOpacity;
            while (timer < duration)
            {
                timer =Mathf.Min(duration,timer + Time.unscaledDeltaTime);

                RenderOpacity = Mathf.Lerp(start, opacity, fadingCurve.Evaluate(timer / duration));
                yield return null;
            }
            OnFinished?.Invoke();
        }
    }
}

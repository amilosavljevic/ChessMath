using System;
using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public abstract class TweenAnimationBase
    {
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public Func<float, float> Easing { get; set; }

        protected TweenAnimationBase()
        {
            StartValue = 0f;
            EndValue = 1f;
            Easing = EasingFunctions.Linear;
        }

        public void Seek(float progress)
        {
            progress = Mathf.Clamp01(progress);
            var newValue = SampleValue(progress);
            OnSeek(progress, newValue);
        }

        protected double SampleValue(float progress) =>
            StartValue + (EndValue - StartValue) * Easing(progress);

        protected abstract void OnSeek(float progress, double newValue);
    }
}
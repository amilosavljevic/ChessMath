using System;

namespace Solitaire.Common.Coroutines
{
    public class TweenAnimation : TweenAnimationBase, IAnimation
    {
        public Action<double> Update { get; set; }
        public TimeSpan Duration { get; set; }

        public TweenAnimation() =>
            Duration = TimeSpan.FromSeconds(1);

        public TweenAnimation(Action<double> updateCallback) : this() =>
            Update = updateCallback;

        protected override void OnSeek(float progress, double newValue) =>
            Update?.Invoke(newValue);
    }
}
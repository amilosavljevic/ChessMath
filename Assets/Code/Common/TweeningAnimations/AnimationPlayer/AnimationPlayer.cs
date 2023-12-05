using System;
using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public enum AnimationState {Stopped, Playing, Finished}


    public class AnimationPlayer : IAnimationPlayer
    {
        public event Action<AnimationState> StateChanged;
        public event Action Finished;

        public const float DefaultSpeed = 1f;
        public const bool ShouldLoopOnDefault = false;

        public readonly IAnimation Animation;

        public AnimationState State
        {
            get => state;
            private set
            {
                if (state == value)
                    return;

                state = value;
                StateChanged?.Invoke(value);
                if (state == AnimationState.Finished) Finished?.Invoke();
            }
        }

        public bool ShouldLoop { get; set; }
        public float Speed { get; set; }
        private float progress;
        private AnimationState state;

        public AnimationPlayer (IAnimation animation)
        {
            this.Animation = animation;
            State = AnimationState.Stopped;
            ShouldLoop = ShouldLoopOnDefault;
            Speed = DefaultSpeed;
        }

        public TimeSpan Time
        {
            get => TimeSpan.FromSeconds (progress * Duration.TotalSeconds);
            set => Progress = (float)(value.TotalSeconds / Duration.TotalSeconds);
        }

        public float Progress
        {
            get => progress;
            set
            {
                value = Mathf.Clamp01(value);
                progress = value;
                Animation.Seek(value);
            }
        }

        public TimeSpan Duration =>
            Animation.Duration;

        public AnimationPlaybackDirection Direction
        {
            get => Speed < 0f ? AnimationPlaybackDirection.Reverse : AnimationPlaybackDirection.Normal;
            set => Speed = value == AnimationPlaybackDirection.Normal
                    ? Mathf.Abs(Speed)
                    : -Mathf.Abs(Speed);
        }

        /// <summary>
        /// Continue playing animation from current position. If animation is finished, it will reset it first.
        /// </summary>
        public void Play()
        {
            if (State == AnimationState.Playing)
                return;

            if (State == AnimationState.Finished)
                Reset();

            State = AnimationState.Playing;
            OnStarted();
        }

        protected virtual void OnStarted() {}

        /// <summary>
        /// Stop playing animation and keep current position.
        /// </summary>
        public void Stop()
        {
            if (State != AnimationState.Playing)
                return;

            State = AnimationState.Stopped;
            OnStopped();
        }

        protected virtual void OnStopped() { }

        /// <summary>
        /// Fast forward to end and stop animation.
        /// </summary>
        public void Finish()
        {
            if (State == AnimationState.Finished)
                return;

            Progress = EndProgress;

            OnStopped();
            State = AnimationState.Finished;
        }

        private void Reset()
        {
            Progress = Speed < 0f ? 1f : 0f;
        }

        public void Tick(TimeSpan delta)
        {
            if (State != AnimationState.Playing)
                return;

            var newTime = Time + delta;

            if (newTime >= Duration && !ShouldLoop)
            {
                Finish();
                return;
            }

            if (newTime >= Duration && ShouldLoop)
            {
                newTime = TimeSpan.FromSeconds(newTime.TotalSeconds % Duration.TotalSeconds);
            }

            Time = newTime;
        }

        private float EndProgress =>
            Speed >= 0f ? 1f : 0f;

        public virtual void Dispose() =>
            Stop();
    }
}

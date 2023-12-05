using System;

namespace Solitaire.Common.Coroutines
{
    public interface IAnimationPlayer : IDisposable
    {
        event Action<AnimationState> StateChanged;
        event Action Finished;
        AnimationState State { get; }
        float Progress { get; set; }
        TimeSpan Time { get; set; }
        TimeSpan Duration { get; }
        AnimationPlaybackDirection Direction { get; set; }

        /// <summary>
        /// Continue playing animation from current position. If animation is finished, it will reset it first.
        /// </summary>
        void Play();

        /// <summary>
        /// Stop playing animation and keep current position.
        /// </summary>
        void Stop();

        /// <summary>
        /// Fast forward to end and stop animation.
        /// </summary>
        void Finish();
    }

    public static class IAnimationPlayerExtension
    {
        public static IAnimationPlayer OnFinished(this IAnimationPlayer anim, Action callback)
        {
            anim.Finished += callback;
            return anim;
        }
    }
}

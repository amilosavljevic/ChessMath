using System;

namespace Solitaire.Common.Coroutines
{
    public interface IAnimation
    {
        /// Seek to relative time/progress (normalized from 0f-1f).
        public void Seek(float progress);
        public TimeSpan Duration { get; }
    }
    
    public static class AnimationExtensions
    {
        /// Seek to time.
        public static void SeekToTime(this IAnimation animation, TimeSpan time) =>
            animation.Seek((float)(time.TotalSeconds / animation.Duration.TotalSeconds));
        
        public static T WarmUp<T>(this T animation) where T : IAnimation
        {
            animation.Seek(0);
            return animation;
        }
    }
}
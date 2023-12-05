namespace Solitaire.Common.Coroutines
{
    public enum AnimationPlaybackDirection {Normal, Reverse}
    
    public static class AnimationPlaybackDirectionExtensions
    {
        /*
        /// <summary>
        /// Make sure we are going in direction we want and start animation only if that make sense.
        /// For example it will not start animation that is already at the end and desired direction is "Normal".
        /// </summary>
        public static void PlayInDirection(this AnimationPlayer animation, AnimationPlaybackDirection direction)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (animation.Progress == 1f && direction == AnimationPlaybackDirection.Normal)
                return;
            
            if (animation.Progress == 0f && direction == AnimationPlaybackDirection.Reverse)
                return;

            animation.Direction = direction;
            animation.Play();
        }*/
    }
}
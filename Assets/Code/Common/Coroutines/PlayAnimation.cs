using System;
using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public class PlayAnimation : Coroutine
    {
        private readonly Animation animation;
        private readonly float targetDuration;
        private readonly AnimationClip clip;

        public PlayAnimation(Animation animation, AnimationClip clip)
            :this(animation, clip, null){}

        public PlayAnimation(Animation animation, float targetDuration)
            :this(animation, targetDuration,  animation.clip,  null){}

        public PlayAnimation(Animation animation, float targetDuration, AnimationClip clip)
            :this(animation, targetDuration, clip, null){}

        public PlayAnimation(Animation animation, float duration, Action onFinished)
            :this(animation, duration, animation.clip, onFinished){}

        public PlayAnimation(Animation animation, AnimationClip clip, Action onFinished)
            :this(animation, clip.length, clip, onFinished){}

        private PlayAnimation (Animation animation, float targetDuration, AnimationClip clip, Action onFinished)
        {
            this.animation = animation;
            this.targetDuration = targetDuration;
            this.clip = clip;
            Done += onFinished;
        }

        protected override void OnStart()
        {
            if (targetDuration == 0.0f)
            {
                Stop();
                return;
            }
            
            animation.clip = clip;
            var clipDuration = animation.clip.length;
            var targetSpeed = clipDuration / targetDuration;

            foreach (UnityEngine.AnimationState state in animation)
                state.speed = targetSpeed;

            animation.Sample();
            animation.Play();
        }

        protected override void Update()
        {
            base.Update();
            if (!animation.isPlaying) Stop();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            if (animation.isPlaying)
                animation.Stop();
        }
    }
}
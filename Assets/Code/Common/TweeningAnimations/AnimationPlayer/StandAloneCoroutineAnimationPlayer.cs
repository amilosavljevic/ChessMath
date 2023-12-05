using System;
using System.Collections;
using ChessMath.Shared.UnityCommon.Utils;
using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public class StandAloneCoroutineAnimationPlayer : AnimationPlayer, IEnumerator
    {
        private readonly MonoBehaviour target;
        private UnityEngine.Coroutine coroutine;

        public StandAloneCoroutineAnimationPlayer(IAnimation animation, MonoBehaviour target) : base(animation)
        {
            this.target = target;
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            coroutine = target.StartCoroutine(this);
        }

        protected override void OnStopped()
        {
            base.OnStopped();

            if (target != null && coroutine != null)
                target.StopCoroutine(coroutine);
            coroutine = null;
        }

        public bool MoveNext()
        {
            Tick(TimeSpan.FromSeconds(UnityEngine.Time.deltaTime));
            return State == AnimationState.Playing;
        }

        public void Reset() => throw new NotImplementedException();
        public object Current => null;
    }

    public static class CoroutineAnimationPlayerExtensions
    {
        public static StandAloneCoroutineAnimationPlayer Start(this IAnimation animation) =>
            Start(animation, MonoBehaviourProxy.I);

        public static StandAloneCoroutineAnimationPlayer Start(this IAnimation animation, MonoBehaviour targetToAttacheTo)
        {
            var player = new StandAloneCoroutineAnimationPlayer(animation, targetToAttacheTo);
            player.Play();
            return player;
        }

        public static IEnumerator Wait(this StandAloneCoroutineAnimationPlayer animationPlayer) =>
            new WaitUntil(() => animationPlayer.State == AnimationState.Finished);
    }
}

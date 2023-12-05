using System;
using System.Collections;

namespace Solitaire.Common.Coroutines
{
    /// <summary>
    /// Animation player that you can return from your coroutine and it will be ticked by whoever is
    /// ticking your conroutine.
    /// </summary>
    public class InlineCoroutineAnimationPlayer : AnimationPlayer, IEnumerator
    {
        public InlineCoroutineAnimationPlayer(IAnimation animation) : base(animation)
        {
        }

        public bool MoveNext()
        {
            Tick(TimeSpan.FromSeconds(UnityEngine.Time.deltaTime));
            return State == AnimationState.Playing;
        }

        public void Reset() => throw new NotImplementedException();
        public object Current => null;
    }

    public static class InlineCoroutineAnimationPlayerExtensions
    {
        public static InlineCoroutineAnimationPlayer StartAndWait(this IAnimation animation)
        {
            var player = new InlineCoroutineAnimationPlayer(animation);
            player.Play();
            return player;
        }
    }
}

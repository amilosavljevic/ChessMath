using System;
using System.Collections.Generic;

namespace Solitaire.Common.Coroutines
{
    public delegate void SeqenceUpdateFunction<in T>(T element, int index, double value);
    
    public abstract class CompositeTweenAnimation<T> : TweenAnimationBase, IAnimation
    {
        protected IReadOnlyList<T> Targets { get; }
        public SeqenceUpdateFunction<T> UpdateFunction { get; set; }

        protected CompositeTweenAnimation(IReadOnlyList<T> targets, SeqenceUpdateFunction<T> updateFunction)
        {
            Targets = targets;
            UpdateFunction = updateFunction;
        }

        public abstract TimeSpan Duration { get; }
    }
}
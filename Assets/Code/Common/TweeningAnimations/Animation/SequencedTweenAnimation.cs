using System;
using System.Collections.Generic;

namespace Solitaire.Common.Coroutines
{
    public class SequencedTweenAnimation<T> : CompositeTweenAnimation<T>
    {
        public TimeSpan DurationPerItem { get; set; } = TimeSpan.FromSeconds(1f);
        public TimeSpan StartDelayPerItem { get; set; } = TimeSpan.FromSeconds(0f);

        public SequencedTweenAnimation(IReadOnlyList<T> targets, SeqenceUpdateFunction<T> updateFunction = null)
            : base(targets, updateFunction)
        {
        }

        protected override void OnSeek(float progress, double newValue)
        {
            var time = Duration.TotalSeconds * progress;
            var iterationDuration = DurationPerItem.TotalSeconds;

            for (var index = 0; index < Targets.Count; index++)
            {
                var target = Targets[index];
                var localWindowStartTime = index * StartDelayPerItem.TotalSeconds;
                var localWindowEndTime = localWindowStartTime + iterationDuration;
                var localTime = Clamp(time, localWindowStartTime, localWindowEndTime);
                var localProgress = (localTime - localWindowStartTime) / iterationDuration;
                UpdateFunction(target, index, SampleValue((float)localProgress));
            }
        }

        public override TimeSpan Duration =>
            Targets.Count == 0
                ? TimeSpan.Zero
                : Multiply(StartDelayPerItem, Targets.Count - 1) + DurationPerItem;


        public TimeSpan TotalDuration
        {
            get => Duration;
            set
            {
                var durationPerChild = value.TotalSeconds - StartDelayPerItem.TotalSeconds * (Targets.Count - 1);
                if (durationPerChild < 0) durationPerChild = 0;
                DurationPerItem = TimeSpan.FromSeconds(durationPerChild);
            }
        }

        // TODO: Delete when we update to .netstandard2.1
        private static TimeSpan Multiply(TimeSpan span, double value) =>
            TimeSpan.FromSeconds(span.TotalSeconds * value);

        // TODO: Delete when we update to .netstandard2.1
        private static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}

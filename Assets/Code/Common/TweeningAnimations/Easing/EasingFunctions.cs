using System;

namespace Solitaire.Common.Coroutines
{
    public static class EasingFunctions
    {
        public static readonly Func<float, float> Linear = x => x;
        public static readonly EasingFunction Quadratic = EasingFunction.FromEaseIn( x => x*x);
        public static readonly EasingFunction Cubic = EasingFunction.FromEaseIn( x => x*x*x);

        public static readonly EasingFunction SinEaseOut =
            EasingFunction.FromEaseOut(t => (float)Math.Sin((t * Math.PI) / 2));

        public static readonly EasingFunction BounceEasing =
            EasingFunction.FromEaseOut(x =>
            {
                if (x <= 0.5)
                    return (float)((x * x) /0.5);
                return (float)((1 - x) / 0.5);
            });
    }
}

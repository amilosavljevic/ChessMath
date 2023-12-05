using System;

namespace Solitaire.Common.Coroutines
{
    public readonly struct EasingFunction
    {
        public readonly Func<float, float> EaseOut;
        public readonly Func<float, float> EaseIn;

        public EasingFunction(Func<float, float> easeIn) : this(easeIn, GetEaseOutFrom(easeIn))
        {
        }
        
        public EasingFunction(Func<float, float> easeIn, Func<float, float> easeOut)
        {
            EaseOut = easeOut;
            EaseIn = easeIn;
        }
        
        private static Func<float, float> GetEaseOutFrom(Func<float, float> easeOut) =>
            t => 1 - easeOut(1 - t);

        public static EasingFunction FromEaseIn(Func<float, float> easeIn) =>
            new EasingFunction(easeIn);
        
        public static EasingFunction FromEaseOut(Func<float, float> easeOut) =>
            new EasingFunction(GetEaseOutFrom(easeOut), easeOut);
    }
}
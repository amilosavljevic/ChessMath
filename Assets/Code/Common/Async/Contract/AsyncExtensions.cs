using System;

namespace ChessMath.Shared.Common
{
    public static class AsyncExtensions
    {
        public static bool IsSuccess(this IAsync operation) =>
            operation.State == AsyncState.Succeeded;

        public static bool IsFailure(this IAsync operation) =>
            operation.State == AsyncState.Failed;

        public static bool IsStillInProgress(this IAsync operation) =>
            operation.State == AsyncState.InProgress;

        public static TAsync OnSuccess<TAsync>(this TAsync op, Action<IAsync> action)
            where TAsync : IAsync
        {
            op.StateChanged += a =>
            {
                if (a.IsSuccess()) action(a);
            };
                
            return op;
        }
        
        public static TAsync OnSuccess<TAsync>(this TAsync op, Action action)
            where TAsync : IAsync
        {
            op.StateChanged += a =>
            {
                if (a.IsSuccess()) action();
            };
            return op;
        }
    }
}
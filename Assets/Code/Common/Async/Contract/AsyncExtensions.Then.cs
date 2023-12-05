using System;

namespace ChessMath.Shared.Common
{
    public static class AsyncExtensionsThen
    {
        public static IAsync Then(this IAsync originalOperation, Action nextAction)
        {
            var resultingOperation = new Async();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.FinishSameAs(op);
                    return;
                }

                try
                {
                    nextAction();
                    resultingOperation.Finish();
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync Then<TOriginal>(this TOriginal originalOperation, Action<TOriginal> nextAction)
            where TOriginal : IAsync
        {
            var resultingOperation = new Async();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.FinishSameAs(op);
                    return;
                }

                try
                {
                    nextAction((TOriginal)op);
                    resultingOperation.Finish();
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<T> ThenReturn<T>(this IAsync originalOperation, Func<T> resultFactory)
        {
            var resultingOperation = new Async<T>();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.Fail(op.Context);
                    return;
                }

                try
                {
                    var result = resultFactory();
                    resultingOperation.Finish(result);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<TOut> ThenReturn<TIn, TOut>(this IAsync<TIn> originalOperation, Func<TIn, TOut> resultConverter)
        {
            var resultingOperation = new Async<TOut>();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.Fail(op.Context);
                    return;
                }

                try
                {
                    var result = resultConverter(((IAsync<TIn>)op).Result);
                    resultingOperation.Finish(result);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync Then(this IAsync originalOperation, Func<IAsync> nextActionFactory)
        {
            var resultingOperation = new Async();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.FinishSameAs(op);
                    return;
                }

                try
                {
                    var next = nextActionFactory();
                    next.StateChanged += t => resultingOperation.FinishSameAs(t);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<T> Then<T>(this IAsync originalOperation, Func<IAsync<T>> nextActionFactory)
        {
            var resultingOperation = new Async<T>();

            originalOperation.StateChanged += op =>
            {
                if (!op.IsSuccess())
                {
                    resultingOperation.Fail(op.Context);
                    return;
                }

                try
                {
                    var next = nextActionFactory();
                    next.StateChanged += t => resultingOperation.FinishSameAs((IAsync<T>)t);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<TNext> Then<TAsyncFirst, TNext>(this TAsyncFirst firstOperation, Func<TAsyncFirst, IAsync<TNext>> nextActionFactory)
            where TAsyncFirst : IAsync
        {
            var resultingOperation = new Async<TNext>();

            firstOperation.StateChanged += op =>
            {
                if (op.IsFailure())
                {
                    resultingOperation.Fail(op.Context);
                    return;
                }

                try
                {
                    var next = nextActionFactory((TAsyncFirst)op);
                    next.StateChanged += n => resultingOperation.FinishSameAs((IAsync<TNext>)n);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync Then<TAsyncFirst>(this TAsyncFirst firstOperation, Func<TAsyncFirst, IAsync> continuationFactory)
            where TAsyncFirst : IAsync
        {
            var resultingOperation = new Async();

            firstOperation.StateChanged += op =>
            {
                if (op.IsFailure())
                {
                    resultingOperation.Fail(op.Context);
                    return;
                }

                try
                {
                    var nextTask = continuationFactory((TAsyncFirst)op);
                    nextTask.StateChanged += nTask => resultingOperation.FinishSameAs(nTask);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }
    }
}

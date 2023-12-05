using System;

namespace ChessMath.Shared.Common
{
    public static class AsyncExtensionsFallbackTo
    {
        public static IAsync FallbackTo(this IAsync originalOperation, Action fallbackAction)
        {
            var resultingOperation = new Async();

            originalOperation.StateChanged += op =>
            {
                if (op.IsSuccess())
                {
                    resultingOperation.FinishSameAs(op);
                    return;
                }

                try
                {
                    fallbackAction();
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

        public static IAsync FallbackTo(this IAsync originalOperation, Func<IAsync> fallbackActionFactory)
        {
            var resultingOperation = new Async();

            originalOperation.StateChanged += op =>
            {
                if (op.IsSuccess())
                {
                    resultingOperation.Finish();
                    return;
                }

                // Handle fallback
                try
                {
                    var fallbackOp = fallbackActionFactory();
                    fallbackOp.StateChanged += fOp => resultingOperation.FinishSameAs(fOp);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<TData> FallbackTo<TData>(this IAsync<TData> originalOperation, Func<TData> fallbackResultFactory)
        {
            var resultingOperation = new Async<TData>();

            originalOperation.StateChanged += op =>
            {
                if (op.IsSuccess())
                {
                    resultingOperation.FinishSameAs((IAsync<TData>)op);
                    return;
                }

                // Handle fallback
                try
                {
                    var fallbackResult = fallbackResultFactory();
                    resultingOperation.Finish(fallbackResult);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync<TData> FallbackTo<TData>(this IAsync<TData> originalOperation, Func<IAsync<TData>> fallbackActionFactory)
        {
            var resultingOperation = new Async<TData>();

            originalOperation.StateChanged += op =>
            {
                if (op.IsSuccess())
                {
                    resultingOperation.FinishSameAs((IAsync<TData>)op);
                    return;
                }

                // Handle fallback
                try
                {
                    var fallbackOp = fallbackActionFactory();
                    fallbackOp.StateChanged += fOp => resultingOperation.FinishSameAs((IAsync<TData>)fOp);
                }
                catch (Exception e)
                {
                    resultingOperation.Fail(e);
                    throw;
                }
            };

            return resultingOperation;
        }

        public static IAsync IgnoreFailure(this IAsync originalOperation)
        {
            var resultingOperation = new Async();
            originalOperation.StateChanged += _ => resultingOperation.Finish();
            return resultingOperation;
        }
    }
}

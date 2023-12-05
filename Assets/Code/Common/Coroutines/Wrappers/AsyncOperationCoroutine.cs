using ChessMath.Shared.Common;

namespace Solitaire.Common.Coroutines
{
    public class AsyncOperationCoroutine : Coroutine
    {
        private readonly IAsync asyncOperation;

        public AsyncOperationCoroutine(IAsync asyncOperation) =>
            this.asyncOperation = asyncOperation;

        protected override void Update()
        {
            if (!asyncOperation.IsStillInProgress()) Stop();
        }
    }

    public static class AsyncOperationCoroutineExtensions
    {
        public static AsyncOperationCoroutine Wait(this IAsync asyncOperation) =>
            new AsyncOperationCoroutine(asyncOperation);
        
        public static AsyncOperationCoroutine Wait(this IAsync asyncOperation, out IAsync result) =>
            new AsyncOperationCoroutine(result = asyncOperation);

        public static AsyncOperationCoroutine Wait<T>(this IAsync<T> asyncOperation, out IAsync<T> operation)
        {
            operation = asyncOperation;
            return new AsyncOperationCoroutine(asyncOperation);
        }

        public static AsyncOperationCoroutine ToCoroutine(this IAsync asyncOperation) =>
            new AsyncOperationCoroutine(asyncOperation);
    }
}
using System;

namespace ChessMath.Shared.Common
{
    public delegate void AsyncStateChangedDelegate(IAsync async);

    public class Async : AsyncBase
    {
        public void FinishSameAs(IAsync alreadyFinishedTask)
        {
            if (alreadyFinishedTask.State == AsyncState.InProgress)
                throw new InvalidOperationException("Passed task is not finished");

            SetState(alreadyFinishedTask.State, alreadyFinishedTask.Context);
        }

        public void Finish() =>
            SetState(AsyncState.Succeeded);

        public void Fail(object context = null) =>
            SetState(AsyncState.Failed, context);

        public static Async Finished()
        {
            var op = new Async();
            op.Finish();
            return op;
        }

        public static Async<T> Finished<T>(T result)
        {
            var op = new Async<T>();
            op.Finish(result);
            return op;
        }

        public static Async Failed(object context = null)
        {
            var op = new Async();
            op.Fail(context);
            return op;
        }
    }

    public class Async<T> : AsyncBase, IAsync<T>
    {
        private T result;

        public T Result
        {
            get => State == AsyncState.Succeeded
                ? result
                : throw new InvalidOperationException("Result is not ready.");
            private set => result = value;
        }

        public void FinishSameAs(IAsync<T> alreadyFinishedTask)
        {
            if (alreadyFinishedTask.State == AsyncState.InProgress)
                throw new InvalidOperationException("Passed task is not finished");

            if (alreadyFinishedTask.IsSuccess())
                Result = alreadyFinishedTask.Result;

            SetState(alreadyFinishedTask.State, alreadyFinishedTask.Context);
        }

        public void Finish(T finishResult)
        {
            Result = finishResult;
            SetState(AsyncState.Succeeded);
        }

        public void Fail(object context = null) =>
            SetState(AsyncState.Failed, context);

        public static Async<T> Finished(T result)
        {
            var op = new Async<T>();
            op.Finish(result);
            return op;
        }

        public static Async<T> Failed(object context = null)
        {
            var op = new Async<T>();
            op.Fail(context);
            return op;
        }

        public static implicit operator Async<T>(T result) =>
            Finished(result);
    }
}

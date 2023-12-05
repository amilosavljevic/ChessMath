using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.AsyncActionScheduling
{
    public class AsyncActionScheduler : IAsyncActionScheduler
    {
        private IAsyncAction currentAction;

        private readonly Queue<IAsyncAction> queue = new Queue<IAsyncAction>();

        public bool IsCurrentlyExecuting =>
            currentAction != null;

        public void Schedule(IAsyncAction action)
        {
            queue.Enqueue(action);

            if (currentAction == null)
                StartNext();
        }

        private void StartNext()
        {
            if (!queue.TryDequeue(out var action))
                return;

            currentAction = action;
            var async = action.Start();
            async.StateChanged += _ => OnActionFinished(action);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void OnActionFinished(IAsyncAction finishedAction)
        {
            if (finishedAction != currentAction)
                throw new Exception("Something is wrong because only one action should start at the same time," +
                                    " an the action that is finished is not current action");
            currentAction = null;
            StartNext();
        }
    }
}

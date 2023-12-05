using System;
using System.Collections;

namespace ChessMath.Shared.Common.AsyncActionScheduling
{
    public class CoroutineAsyncAction : IAsyncAction
    {
        private readonly ICoroutineRunner runner;
        private readonly IEnumerator coroutineFlow;
        public IAsync StartedAction { get; private set; }

        public CoroutineAsyncAction(IContext context, IEnumerator coroutineFlow)
            : this(context.GetService<ICoroutineRunner>(), coroutineFlow) { }

        public CoroutineAsyncAction(ICoroutineRunner runner, IEnumerator coroutineFlow)
        {
            this.runner = runner;
            this.coroutineFlow = coroutineFlow;
        }

        public IAsync Start()
        {
            if (StartedAction != null)
                throw new InvalidOperationException("Action already started!");

            return StartedAction = runner.StartCoroutineAsync(coroutineFlow);
        }
    }
}

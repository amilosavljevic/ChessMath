using System.Collections;

namespace ChessMath.Shared.Common
{
    public interface ICoroutineRunner
    {
        CoroutineHandle StartCoroutine(IEnumerator routine);
        void StopCoroutine(object context);
    }

    public static class CoroutineRunnerExtensions
    {
        public static CoroutineHandle StartCoroutine(this IContext context, IEnumerator routine) =>
            context.GetService<ICoroutineRunner>().StartCoroutine(routine);

        public static AsyncCoroutineHandle StartCoroutineAsync(this IContext context, IEnumerator originalFlow) =>
            context.GetService<ICoroutineRunner>().StartCoroutineAsync(originalFlow);


        // TODO: Move this as a part of ICoroutineRunner interface so that we can use custom coroutine runner
        // and handle any potential exception that coroutine might throw.
        public static AsyncCoroutineHandle StartCoroutineAsync(this ICoroutineRunner runner, IEnumerator originalFlow)
        {
            var async = new Async();
            var handle = runner.StartCoroutine(ExtendedFlow(async, originalFlow));
            return new AsyncCoroutineHandle(handle, async);
        }

        private static IEnumerator ExtendedFlow(Async async, IEnumerator originalFlow)
        {
            yield return originalFlow;
            async.Finish();
        }
    }
}

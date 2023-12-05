namespace ChessMath.Shared.Common
{
    public interface IAsyncActionScheduler
    {
        bool IsCurrentlyExecuting { get; }
        void Schedule (IAsyncAction action);
    }
}

namespace ChessMath.Shared.Common
{
    public interface IAsync
    {
        object Context { get; }
        event AsyncStateChangedDelegate StateChanged;
        
        public AsyncState State { get; }
    }

    public interface IAsync<out T> : IAsync
    {
        T Result { get; }
    }
}
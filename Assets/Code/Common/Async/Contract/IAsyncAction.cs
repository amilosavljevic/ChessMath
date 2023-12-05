namespace ChessMath.Shared.Common
{
    public interface IAsyncAction
    {
        public IAsync StartedAction { get; }
        public IAsync Start();
    }
}

namespace ChessMath.Shared.Common.EventBusNs
{
    public interface IBusPreProcessor
    {
        public void PreProcess<T>(T message) where T:IEvent;
    }
}
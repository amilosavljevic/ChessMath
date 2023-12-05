namespace ChessMath.Shared.Common.EventBusNs
{
    public interface IBusPostProcessor
    {
        public void PostProcess<T>(T message) where T:IEvent;
    }
}

namespace ChessMath.Shared.Common.EventBusNs
{
    public class ForwardingEventBus : EventBusDecorator
    {
        private readonly IEventBus eventBus;
        private readonly IEventBus busToForwardTo;

        public ForwardingEventBus(IEventBus eventBus, IEventBus busToForwardTo)
            : base(eventBus)
        {
            this.busToForwardTo = busToForwardTo;
        }

        public override void Publish<T>(T message)
        {
            base.Publish(message);
            busToForwardTo.Publish(message);
        }
    }

    public static class ForwardingEventBusExtensions
    {
        public static ForwardingEventBus WithForwardingTo(this IEventBus original, IEventBus busToForwardTo) =>
            new ForwardingEventBus(original, busToForwardTo);
    }
}

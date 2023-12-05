using System;
using System.Collections.Generic;
using ChessMath.Shared.Common.AppContextNs;

namespace ChessMath.Shared.Common.EventBusNs.DelayedEventBus
{
    public class StallingEventBus : EventBusDecorator
    {
        private readonly Queue<Action> queuedPublishActions = new Queue<Action>();
        private int blockingSemaphoreCount = 0;

        public StallingEventBus(IEventBus eventBus) : base(eventBus)
        {
        }

        public bool IsBlocking =>
            blockingSemaphoreCount > 0;

        public void BlockOnce() =>
            blockingSemaphoreCount++;

        public bool UnBlockOnce()
        {
            if (!IsBlocking)
                throw new InvalidOperationException("Blocking/Unblocking count miss-match");

            blockingSemaphoreCount--;

            if (blockingSemaphoreCount > 0)
                return false;

            PublishAll();
            return true;
        }

        public override void Publish<T>(T message)
        {
            if (!IsBlocking)
            {
                base.Publish(message);
                return;
            }

            queuedPublishActions.Enqueue(() => base.Publish(message));
        }

        private void PublishAll()
        {
            // If during "unblocking" some of the event pauses queue again =>
            // we do not want to continue dispatching events anymore.
            while (queuedPublishActions.Count > 0 && !IsBlocking)
            {
                var action = queuedPublishActions.Dequeue();
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }

    public static class StallingEventBusExtensions
    {
        public static StallingEventBus WithEventsStalling(this IEventBus eventBus) =>
            new StallingEventBus(eventBus);

        public static IDisposable BlockOnceWithHandle(this StallingEventBus eventBus)
        {
            eventBus.BlockOnce();
            return new LambdaDisposable(() => eventBus.UnBlockOnce());
        }
    }
}

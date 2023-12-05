using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.EventBusNs
{
    internal class EventBusWithProcessors : EventBusDecorator
    {
        public event Action<IEvent> Publishing;
        public event Action<IEvent> Published;

        private readonly List<IBusPreProcessor> preProcessors = new List<IBusPreProcessor>();
        private readonly List<IBusPostProcessor> postProcessors = new List<IBusPostProcessor>();

        public EventBusWithProcessors(IEventBus eventBus) : base(eventBus)
        {
        }

        public override void Publish<T>(T message)
        {
            RunPreProcessors(message);
            NotifyPublishing(message);
            base.Publish(message);
            RunPostProcessors(message);
            NotifyPublished(message);
        }

        public void AddPreProcessor(IBusPreProcessor preProcessor) =>
            preProcessors.Add(preProcessor);

        public void AddPostProcessor(IBusPostProcessor postProcessor) =>
            postProcessors.Add(postProcessor);

        private void RunPreProcessors<T>(T message) where T : IEvent
        {
            foreach (var preProcessor in preProcessors)
                preProcessor.PreProcess(message);
        }

        private void RunPostProcessors<T>(T message) where T : IEvent
        {
            foreach (var postProcessor in postProcessors)
                postProcessor.PostProcess(message);
        }

        private void NotifyPublished(IEvent message) =>
            Published?.Invoke(message);

        private void NotifyPublishing(IEvent message) =>
            Publishing?.Invoke(message);
    }

    public static class PublishHookEventBusExtensions
    {
        public static IEventBus WithPostProcessor(this IEventBus eventBus, IBusPostProcessor postProcessor)
        {
            var processorBus = GetProcessorEventBus(eventBus);
            processorBus.AddPostProcessor(postProcessor);
            return processorBus;
        }

        public static IEventBus WithPreProcessor(this IEventBus eventBus, IBusPreProcessor preProcessor)
        {
            var processorBus = GetProcessorEventBus(eventBus);
            processorBus.AddPreProcessor(preProcessor);
            return processorBus;
        }

        public static IEventBus WithOnPublishingHook(this IEventBus eventBus, Action<IEvent> callback)
        {
            var processorBus = GetProcessorEventBus(eventBus);
            processorBus.Publishing += callback;
            return processorBus;
        }

        public static IEventBus WithOnPublishedHook(this IEventBus eventBus, Action<IEvent> callback)
        {
            var processorBus = GetProcessorEventBus(eventBus);
            processorBus.Published += callback;
            return processorBus;
        }

        private static EventBusWithProcessors GetProcessorEventBus(IEventBus bus) =>
            bus switch
            {
                EventBusWithProcessors processorBus => processorBus,
                _ => new EventBusWithProcessors(bus),
            };
    }
}

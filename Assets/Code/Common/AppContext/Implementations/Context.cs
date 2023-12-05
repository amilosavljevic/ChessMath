using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChessMath.Shared.Common.Events;

namespace ChessMath.Shared.Common.AppContextNs
{
    public class Context : IContext
    {
        protected readonly ServiceCollection Services;
        private IEventBus eventBus;

        public ContextState State { get; private set; }

        public Context(IEventBus eventBus) 
            : this (new ServiceCollection(), eventBus)
        {
        }

        public Context(ServiceCollection services, IEventBus eventBus)
        {
            State = ContextState.NotInitialized;
            Services = services ?? throw new ArgumentNullException(nameof(services));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        protected Context(ServiceCollection services)
        {
            State = ContextState.NotInitialized;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected void SetEventBus(IEventBus bus) =>
            eventBus = bus;

        public IEnumerator Initialize()
        {
            if (State != ContextState.NotInitialized)
                throw new InvalidOperationException($"Context initialization already started. Current State = {State}");

            State = ContextState.Initializing;

            OnInitializing();
            Publish(new ContextInitializationStarted());

            yield return InitializeServices();

            State = ContextState.Initialized;
            OnInitializationFinished();
            Publish(new ContextInitializationFinished());
        }

        protected virtual void OnInitializing() {}

        private IEnumerator InitializeServices()
        {
            var gameServices = Services
                .GetAllServices()
                .OfType<IGameService>()
                .Where(gs => !gs.IsInitialized)
                .ToHashSet();

            var coroutineStarter = GetService<ICoroutineRunner>();

            // TODO: If we use WaitAll here there's no need to start each coroutine as individually
            foreach (var service in gameServices)
                coroutineStarter.StartCoroutine(service.Initialize());

            yield return WaitUntilAllServicesAreInitialized(gameServices);
        }

        private IEnumerator WaitUntilAllServicesAreInitialized(HashSet<IGameService> gameServices)
        {
            while(true)
            {
                gameServices.RemoveWhere(s => s.IsInitialized);
                if (gameServices.Count == 0) yield break;

                yield return null;
            }
        }

        protected virtual void OnInitializationFinished() {}

        public virtual void Dispose()
        {
            if (State != ContextState.Initialized)
                return;

            State = ContextState.Disposed;

            foreach (var service in Services.GetAllServices())
            {
                if (service is IDisposable disposableService)
                {
                    try
                    {
                        disposableService.Dispose();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }

        public object GetService(Type serviceType) =>
            Services.GetService(serviceType);

        public T GetService<T>() where T : class
        {
            var type = typeof(T);

            return Services.GetService(type) as T
                   ?? throw new InvalidOperationException($"Cannot find Service of type: {type}");
        }

        public T TryGetService<T>() where T : class =>
            Services.GetService(typeof(T)) as T;

        public void Publish<T>(T message) where T : IEvent =>
            eventBus.Publish(message);

        public void Subscribe<T>(Action<T> handler) where T : IEvent =>
            eventBus.Subscribe(handler);

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent =>
            eventBus.Unsubscribe(handler);
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Infrastructure {
    /// <summary>
    /// Event dispatcher to handle notifying subscribers when an event occurs.
    /// </summary>
    public class EventBus : IEventBus {
        #region Fields
        private IServiceProvider serviceProvider;

        private Dictionary<Type, List<Func<IDomainEvent, Task>>> asyncDynamicListeners;

        private Dictionary<Type, List<Action<IDomainEvent>>> dynamicListeners;
        #endregion

        #region Constructor(s)
        public EventBus(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;

            this.asyncDynamicListeners = new Dictionary<Type, List<Func<IDomainEvent, Task>>>();
            this.dynamicListeners = new Dictionary<Type, List<Action<IDomainEvent>>>();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Dispatch an event to any handler that wants to know about it.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch.</param>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        public async Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent {
            Type eventType = typeof(TEvent);
            IEnumerable<IDomainEventHandler<TEvent>> handlers = serviceProvider.GetServices<IDomainEventHandler<TEvent>>(); //  serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>).GetType()) as IDomainEventHandler<TEvent>[];

            foreach (IDomainEventHandler<TEvent> handler in handlers) {
                await handler.Handle(domainEvent);
            }

            List<Func<IDomainEvent, Task>>? asyncListeners;

            if (asyncDynamicListeners.TryGetValue(eventType, out asyncListeners)) {
                foreach (Func<IDomainEvent, Task> listener in asyncListeners) {
                    await listener(domainEvent);
                }
            }

            List<Action<IDomainEvent>>? syncListeners;
            if (dynamicListeners.TryGetValue(eventType, out syncListeners)) {
                foreach (Action<IDomainEvent> listener in syncListeners) {
                    listener(domainEvent);
                }
            }
        }

        public void ListenAsync<TEvent>(Func<IDomainEvent, Task> listener) where TEvent : class, IDomainEvent {
            List<Func<IDomainEvent, Task>>? listeners;
            Type eventType = typeof(TEvent);

            // If the dictionary doesn't hold any listeners for an event, create a new list for them.
            if (!asyncDynamicListeners.TryGetValue(eventType, out listeners)) {
                listeners = new List<Func<IDomainEvent, Task>>();
                asyncDynamicListeners.Add(eventType, listeners);
            }

            listeners.Add(listener);
        }

        public void Listen<TEvent>(Action<IDomainEvent> listener) where TEvent : class, IDomainEvent {
            List<Action<IDomainEvent>>? listeners;
            Type eventType = typeof(TEvent);

            // If the dictionary doesn't hold any listeners for an event, create a new list for them.
            if (!dynamicListeners.TryGetValue(eventType, out listeners)) {
                listeners = new List<Action<IDomainEvent>>();
                dynamicListeners.Add(eventType, listeners);
            }

            listeners.Add(listener);
        }
        #endregion
    }
}

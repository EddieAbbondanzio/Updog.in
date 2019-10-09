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
        #endregion

        #region Constructor(s)
        public EventBus(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Dispatch an event to any handler that wants to know about it.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch.</param>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        public async Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent {
            IEnumerable<IDomainEventHandler<TEvent>> handlers = serviceProvider.GetServices<IDomainEventHandler<TEvent>>(); //  serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>).GetType()) as IDomainEventHandler<TEvent>[];

            foreach (IDomainEventHandler<TEvent> handler in handlers) {
                await handler.Handle(domainEvent);
            }
        }
        #endregion
    }
}

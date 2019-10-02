using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Application;

namespace Updog.Infrastructure {
    /// <summary>
    /// Event dispatcher to handle notifying subscribers when an event occurs.
    /// </summary>
    public class EventBus : IEventBus {
        #region Fields
        private IDatabase database;
        private IServiceProvider serviceProvider;
        #endregion

        #region Constructor(s)
        public EventBus(IDatabase database, IServiceProvider serviceProvider) {
            this.database = database;
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
            IDomainEventHandler<TEvent>[]? handlers = serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>[])) as IDomainEventHandler<TEvent>[];

            if (handlers?.Length > 0) {
                for (int i = 0; i < handlers.Length; i++) {
                    await handlers[i].Handle(domainEvent);
                }
            }
        }
        #endregion
    }
}

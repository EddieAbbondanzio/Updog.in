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
            // Get the handlers we need to notify of the event.
            IDomainEventHandler<TEvent>[]? handlers = serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>[])) as IDomainEventHandler<TEvent>[];
            Queue<IDomainEventHandler<TEvent>> deferredHandlers = new Queue<IDomainEventHandler<TEvent>>();

            if (handlers?.Length > 0) {
                using (var context = database.GetContext()) {
                    using (var transaction = context.Connection.BeginTransaction()) {

                        // Figure out if the handler should be deferred, or handled instantly.
                        for (int i = 0; i < handlers.Length; i++) {
                            if (handlers[i].IsDeferred) {
                                deferredHandlers.Enqueue(handlers[i]);
                            } else {
                                await handlers[i].Handle(domainEvent);
                            }
                        }

                        transaction.Commit();

                        // Now handle the deferred ones (if any).
                        if (deferredHandlers.Count > 0) {
                            await HandleDeferred(domainEvent, deferredHandlers);
                        }
                    }
                }
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Process the queue of deferred handlers to notify.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch.</param>
        /// <param name="deferredHandlers">The handlers to notify.</param>
        /// <typeparam name="TEvent">Event type.</typeparam>
        private async Task HandleDeferred<TEvent>(TEvent domainEvent, Queue<IDomainEventHandler<TEvent>> deferredHandlers) where TEvent : IDomainEvent {
            IDomainEventHandler<TEvent> handler;

            while (deferredHandlers.TryDequeue(out handler)) {
                await handler.Handle(domainEvent);
            }
        }
        #endregion
    }
}

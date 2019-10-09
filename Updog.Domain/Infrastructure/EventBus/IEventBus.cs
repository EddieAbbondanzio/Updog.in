using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    /// <summary>
    /// Bus for propogating events to help keep modules loosely coupled with each other.
    /// </summary>
    public interface IEventBus {
        #region Publics
        /// <summary>
        /// Dispatch the event to any handlers that may be waiting for it.
        /// </summary>
        /// <param name="domainEvent">The event to raise.</param>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent;

        /// <summary>
        /// Create an async dynamic listener that will be notified of any events of a specific type.
        /// </summary>
        /// <typeparam name="TEvent">The event type to be notified of.</typeparam>
        void ListenAsync<TEvent>(Func<IDomainEvent, Task> listener) where TEvent : class, IDomainEvent;

        /// <summary>
        /// Create a dynamic listener that will be notified of a specific type.
        /// </summary>
        /// <typeparam name="TEvent">The even type to be notified of.</typeparam>
        void Listen<TEvent>(Action<IDomainEvent> listenger) where TEvent : class, IDomainEvent;
        #endregion
    }
}
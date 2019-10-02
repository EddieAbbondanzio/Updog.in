using System.Threading.Tasks;

namespace Updog.Application {
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
        Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
        #endregion
    }
}
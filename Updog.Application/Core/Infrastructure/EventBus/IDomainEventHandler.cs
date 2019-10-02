
using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Handler to process a domain event.
    /// </summary>
    /// <typeparam name="TEvent">Event type it can handle.</typeparam>
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent {
        #region Publics
        /// <summary>
        /// Handle a doamin event.
        /// </summary>
        /// <param name="domainEvent">The domain event to handle.</param>
        Task Handle(TEvent domainEvent);
        #endregion
    }
}
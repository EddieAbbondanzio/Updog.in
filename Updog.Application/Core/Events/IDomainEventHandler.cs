
using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Handler to process a domain event.
    /// </summary>
    /// <typeparam name="TEvent">Event type it can handle.</typeparam>
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent {
        #region Properties
        /// <summary>
        /// If the event handler should be invoked upon transaction completion.
        /// </summary>
        bool IsDeferred { get; }
        #endregion

        #region Publics
        /// <summary>
        /// Handle the domain event.
        /// </summary>
        Task Handle(TEvent domainEvent);
        #endregion
    }
}
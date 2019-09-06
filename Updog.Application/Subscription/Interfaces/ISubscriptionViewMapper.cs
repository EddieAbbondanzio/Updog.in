using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert subscription entities to views.
    /// </summary>
    public interface ISubscriptionViewMapper : IMapper<Subscription, SubscriptionView> {

    }
}
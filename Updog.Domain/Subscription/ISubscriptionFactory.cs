using Updog.Domain;

namespace Updog.Domain {
    public interface ISubscriptionFactory : IFactory<Subscription> {
        Subscription CreateFor(User user, Space space);
    }
}
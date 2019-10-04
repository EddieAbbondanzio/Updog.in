using Updog.Domain;

namespace Updog.Application {
    public interface ISubscriptionFactory : IFactory<Subscription> {
        Subscription CreateFor(User user, Space space);
    }
}
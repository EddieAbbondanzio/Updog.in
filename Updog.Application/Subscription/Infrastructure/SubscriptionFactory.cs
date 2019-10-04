using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionFactory : ISubscriptionFactory {
        public Subscription CreateFor(User user, Space space) => new Subscription() {
            Space = space,
            User = user
        };
    }
}
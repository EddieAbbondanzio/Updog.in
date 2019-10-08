using Updog.Domain;

namespace Updog.Domain {
    public sealed class SubscriptionFactory : ISubscriptionFactory {
        public Subscription CreateFor(User user, Space space) => new Subscription() {
            SpaceId = space.Id,
            UserId = user.Id
        };
    }
}
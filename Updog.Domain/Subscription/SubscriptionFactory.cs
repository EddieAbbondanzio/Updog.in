using Updog.Domain;

namespace Updog.Domain {
    public sealed class SubscriptionFactory : ISubscriptionFactory {
        public Subscription Create(int id, int userId, int spaceId) => new Subscription(id, userId, spaceId);
        public Subscription CreateFor(User user, Space space) => new Subscription(user.Id, space.Id);
    }
}
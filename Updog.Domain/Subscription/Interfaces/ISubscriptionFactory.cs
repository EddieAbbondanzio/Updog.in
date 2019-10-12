using Updog.Domain;

namespace Updog.Domain {
    public interface ISubscriptionFactory : IFactory<Subscription> {
        Subscription Create(int id, int userId, int spaceId);
        Subscription CreateFor(User user, Space space);
    }
}
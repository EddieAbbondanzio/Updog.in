using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISubscriptionService : IService<Subscription> {
        Task<Subscription> CreateSubscription(SubscriptionCreate create, User user);
        Task<Subscription> DeleteSubscription(string space, User user);
    }
}
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISubscriptionService : IService<Subscription> {
        Task<Subscription> CreateSubscription(SubscriptionCreateData data, User user);
        Task<Subscription> DeleteSubscription(SubscriptionDeleteData data, User user);
    }
}
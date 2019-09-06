using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find the susbcriptions for a user.
    /// </summary>
    public sealed class SubscriptionFinderByUser : IInteractor<User, IEnumerable<SubscriptionView>> {
        #region Fields
        private ISubscriptionRepo _subscriptionRepo;

        private ISubscriptionViewMapper _subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionFinderByUser(ISubscriptionRepo subscriptionRepo, ISubscriptionViewMapper subscriptionMapper) {
            _subscriptionRepo = subscriptionRepo;
            _subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task<IEnumerable<SubscriptionView>> Handle(User u) {
            IEnumerable<Subscription> subs = await _subscriptionRepo.FindByUser(u.Username);

            List<SubscriptionView> views = new List<SubscriptionView>();

            foreach (Subscription s in subs) {
                views.Add(_subscriptionMapper.Map(s));
            }

            return views;
        }
        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find the susbcriptions for a user.
    /// </summary>
    public sealed class SubscriptionFinderByUser : IInteractor<SubscriptionFindByUserParams, IEnumerable<SubscriptionView>> {
        #region Fields
        private IDatabase database;
        private ISubscriptionViewMapper subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionFinderByUser(IDatabase database, ISubscriptionViewMapper subscriptionMapper) {
            this.database = database;
            this.subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task<IEnumerable<SubscriptionView>> Handle(SubscriptionFindByUserParams input) {
            using (var connection = database.GetConnection()) {
                ISubscriptionRepo subRepo = database.GetRepo<ISubscriptionRepo>(connection);

                IEnumerable<Subscription> subs = await subRepo.FindByUser(input.User.Username);
                return subs.Select(s => subscriptionMapper.Map(s));
            }
        }
        #endregion
    }
}
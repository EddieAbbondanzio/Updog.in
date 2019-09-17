using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find the susbcriptions for a user.
    /// </summary>
    public sealed class SubscriptionFinderByUser : Interactor<FindByValueParams<string>, IEnumerable<SubscriptionView>> {
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
        [Validate(typeof(FindByUserValidator))]
        protected override async Task<IEnumerable<SubscriptionView>> HandleInput(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                ISubscriptionRepo subRepo = database.GetRepo<ISubscriptionRepo>(connection);

                IEnumerable<Subscription> subs = await subRepo.FindByUser(input.Value);
                return subs.Select(s => subscriptionMapper.Map(s));
            }
        }
        #endregion
    }
}
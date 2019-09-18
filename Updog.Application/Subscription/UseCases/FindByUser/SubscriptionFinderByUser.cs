using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find the susbcriptions for a user.
    /// </summary>
    public sealed class SubscriptionFinderByUser : Interactor<FindByValueParams<string>, IEnumerable<SpaceView>> {
        #region Fields
        private IDatabase database;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionFinderByUser(IDatabase database, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(FindByUserValidator))]
        protected override async Task<IEnumerable<SpaceView>> HandleInput(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                ISubscriptionRepo subRepo = database.GetRepo<ISubscriptionRepo>(connection);

                IEnumerable<Subscription> subs = await subRepo.FindByUser(input.Value);
                return subs.Select(s => spaceMapper.Map(s.Space));
            }
        }
        #endregion
    }
}
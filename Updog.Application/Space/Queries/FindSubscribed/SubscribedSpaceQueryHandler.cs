using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscribedSpaceQueryHandler : QueryHandler<SubscribedSpaceQuery> {
        #region Fields
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SubscribedSpaceQueryHandler(IDatabase database, ISpaceViewMapper spaceMapper) : base(database) {
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SubscribedSpaceQuery> context) {
            ISubscriptionRepo subRepo = context.Database.GetRepo<ISubscriptionRepo>();

            IEnumerable<Subscription> subs = await subRepo.FindByUser(context.Input.User.Username);
            context.Output.Success(subs.Select(s => spaceMapper.Map(s.Space)));
        }
        #endregion
    }
}
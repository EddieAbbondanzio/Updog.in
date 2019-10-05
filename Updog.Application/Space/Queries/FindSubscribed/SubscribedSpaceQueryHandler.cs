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
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            var spaces = await spaceRepo.FindSubscribed(context.Input.User);
            context.Output.Success(spaces);
        }
        #endregion
    }
}
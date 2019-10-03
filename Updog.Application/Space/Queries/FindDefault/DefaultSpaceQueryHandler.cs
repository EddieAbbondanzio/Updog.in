using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class DefaultSpaceQueryHandler : QueryHandler<DefaultSpaceQuery> {
        #region Fields
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public DefaultSpaceQueryHandler(IDatabase database, ISpaceViewMapper spaceMapper) : base(database) {
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<DefaultSpaceQuery> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();

            IEnumerable<Space> spaces = await spaceRepo.FindDefault();
            context.Output.Success(spaces.Select(s => spaceMapper.Map(s)));
        }
        #endregion
    }
}
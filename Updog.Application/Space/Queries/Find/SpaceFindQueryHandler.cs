using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindQueryHandler : QueryHandler<SpaceFindQuery> {
        #region Fields
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFindQueryHandler(IDatabase database, ISpaceViewMapper spaceMapper) : base(database) {
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SpaceFindQuery> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();

            PagedResultSet<Space> spaces = await spaceRepo.Find(context.Input.Paging?.PageNumber ?? 0, context.Input.Paging?.PageSize ?? Post.PageSize);
            context.Output.Success(new PagedResultSet<SpaceView>(spaces.Select(s => spaceMapper.Map(s)), spaces.Pagination));
        }
        #endregion
    }
}
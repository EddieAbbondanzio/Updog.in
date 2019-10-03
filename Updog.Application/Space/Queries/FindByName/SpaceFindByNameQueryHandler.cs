using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindByNameQueryHandler : QueryHandler<SpaceFindByNameQuery> {
        #region Fields
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFindByNameQueryHandler(IDatabase database, ISpaceViewMapper spaceMapper) : base(database) {
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SpaceFindByNameQuery> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();

            Space? space = await spaceRepo.FindByName(context.Input.Name);

            if (space == null) {
                context.Output.NotFound($"No space with name: {context.Input.Name} found.");
            } else {
                context.Output.Success(spaceMapper.Map(space));
            }
        }
        #endregion
    }
}
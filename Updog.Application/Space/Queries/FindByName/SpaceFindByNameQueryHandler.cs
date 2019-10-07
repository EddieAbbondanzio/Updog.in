using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindByNameQueryHandler : QueryHandler<SpaceFindByNameQuery> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SpaceFindByNameQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SpaceFindByNameQuery> context) {
            SpaceReadView? space = await spaceReader.FindByName(context.Input.Name);

            if (space != null) {
                context.Output.Success(space);
            } else {
                context.Output.NotFound();
            }
        }
        #endregion
    }
}
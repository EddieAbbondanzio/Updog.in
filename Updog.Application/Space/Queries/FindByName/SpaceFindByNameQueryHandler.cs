using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindByNameQueryHandler : QueryHandler<SpaceFindByNameQuery, SpaceReadView?> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SpaceFindByNameQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task<SpaceReadView?> ExecuteQuery(SpaceFindByNameQuery command) => await spaceReader.FindByName(command.Name);
        #endregion
    }
}
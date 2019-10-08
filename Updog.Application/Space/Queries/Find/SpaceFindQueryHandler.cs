using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindQueryHandler : QueryHandler<SpaceFindQuery, PagedResultSet<SpaceReadView>> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SpaceFindQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task<PagedResultSet<SpaceReadView>> ExecuteQuery(SpaceFindQuery command) => await spaceReader.Find(command.Paging);
        #endregion
    }
}
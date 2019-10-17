using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindSpacesUserModeratesQueryHandler : QueryHandler<FindSpacesUserModeratesQuery, IEnumerable<SpaceReadView>> {
        #region Fields
        private ISpaceReader roleReader;
        #endregion

        #region Constructor(s)
        public FindSpacesUserModeratesQueryHandler(ISpaceReader spaceReader) {
            this.roleReader = spaceReader;
        }
        #endregion

        #region Privates
        protected async override Task<IEnumerable<SpaceReadView>> ExecuteQuery(FindSpacesUserModeratesQuery query) => await roleReader.FindSpacesUserModerates(query.Username);
        #endregion
    }
}
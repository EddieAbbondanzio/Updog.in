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
        protected async override Task<Either<IEnumerable<SpaceReadView>, Error>> ExecuteQuery(FindSpacesUserModeratesQuery query) =>
        new Either<IEnumerable<SpaceReadView>, Error>(
            await roleReader.FindSpacesUserModerates(query.Username)
        );
        #endregion
    }
}
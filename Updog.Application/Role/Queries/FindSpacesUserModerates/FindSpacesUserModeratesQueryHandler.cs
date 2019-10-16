using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindSpacesUserModeratesQueryHandler : QueryHandler<FindSpacesUserModeratesQuery, IEnumerable<UserReadView>> {
        #region Fields
        private IRoleReader roleReader;
        #endregion

        #region Constructor(s)
        public FindSpacesUserModeratesQueryHandler(IRoleReader roleReader) {
            this.roleReader = roleReader;
        }
        #endregion

        #region Privates
        protected async override Task<IEnumerable<SpaceReadView>> ExecuteQuery(FindSpacesUserModeratesQuery query) => await roleReader.FindSpacesUserModerates(query.Username);
        #endregion
    }
}
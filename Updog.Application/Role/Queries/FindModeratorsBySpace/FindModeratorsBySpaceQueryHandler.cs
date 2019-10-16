using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindModeratorsBySpaceQueryHandler : QueryHandler<FindModeratorsBySpaceQuery, IEnumerable<UserReadView>> {
        #region Fields
        private IRoleReader roleReader;
        #endregion

        #region Constructor(s)
        public FindModeratorsBySpaceQueryHandler(IRoleReader roleReader) {
            this.roleReader = roleReader;
        }
        #endregion

        #region Privates
        protected async override Task<IEnumerable<UserReadView>> ExecuteQuery(FindModeratorsBySpaceQuery query) => await roleReader.FindModerators(query.Space);
        #endregion
    }
}
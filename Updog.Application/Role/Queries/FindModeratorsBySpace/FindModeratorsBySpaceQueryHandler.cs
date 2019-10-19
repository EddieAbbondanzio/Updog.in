using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindModeratorsBySpaceQueryHandler : QueryHandler<FindModeratorsBySpaceQuery, IEnumerable<UserReadView>> {
        #region Fields
        private IUserReader userReader;
        #endregion

        #region Constructor(s)
        public FindModeratorsBySpaceQueryHandler(IUserReader roleReader) {
            this.userReader = roleReader;
        }
        #endregion

        #region Privates
        protected async override Task<Either<IEnumerable<UserReadView>, Error>> ExecuteQuery(FindModeratorsBySpaceQuery query) =>
        new Either<IEnumerable<UserReadView>, Error>(
            await userReader.FindModerators(query.Space)
        );
        #endregion
    }
}
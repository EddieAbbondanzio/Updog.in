using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindAdminsQueryHandler : QueryHandler<FindAdminsQuery, IEnumerable<UserReadView>> {
        #region Fields
        private IUserReader userReader;
        #endregion

        #region Constructor(s)
        public FindAdminsQueryHandler(IUserReader userReader) {
            this.userReader = userReader;
        }
        #endregion

        #region Privates
        protected async override Task<Either<IEnumerable<UserReadView>, Error>> ExecuteQuery(FindAdminsQuery query) =>
        new Either<IEnumerable<UserReadView>, Error>(
            await userReader.FindAdmins()
        );
        #endregion
    }
}
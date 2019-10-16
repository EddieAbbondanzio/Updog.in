using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindAdminsQueryHandler : QueryHandler<FindAdminsQuery, IEnumerable<UserReadView>> {
        #region Fields
        private IRoleReader roleReader;
        #endregion

        #region Constructor(s)
        public FindAdminsQueryHandler(IRoleReader roleReader) {
            this.roleReader = roleReader;
        }
        #endregion

        #region Privates
        protected async override Task<IEnumerable<UserReadView>> ExecuteQuery(FindAdminsQuery query) => await roleReader.FindAdmins();
        #endregion
    }
}
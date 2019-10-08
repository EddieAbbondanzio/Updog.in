using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQueryHandler : QueryHandler<IsUsernameAvailableQuery, bool> {
        #region Fields
        private IUserReader userReader;
        #endregion

        #region Constructor(s)
        public IsUsernameAvailableQueryHandler(IUserReader userReader) {
            this.userReader = userReader;
        }
        #endregion

        #region Publics
        [Validate(typeof(IsUsernameAvailableQueryValidator))]
        protected async override Task<bool> ExecuteQuery(IsUsernameAvailableQuery command) => (await userReader.FindByUsername(command.Username)) == null;
        #endregion
    }
}
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQueryHandler : QueryHandler<FindUserByUsernameQuery, UserReadView?> {
        #region Fields
        private IUserReader userReader;
        #endregion

        #region Constructor(s)
        public FindUserByUsernameQueryHandler(IUserReader userReader) {
            this.userReader = userReader;
        }
        #endregion

        #region Publics
        [Validate(typeof(FindUserByUsernameQueryValidator))]
        protected async override Task<UserReadView?> ExecuteQuery(FindUserByUsernameQuery command) => await userReader.FindByUsername(command.Username);
        #endregion
    }
}
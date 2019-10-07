using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQueryHandler : QueryHandler<FindUserByUsernameQuery> {
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
        protected async override Task ExecuteQuery(ExecutionContext<FindUserByUsernameQuery> context) {
            UserReadView? user = await userReader.FindByUsername(context.Input.Username);

            if (user == null) {
                context.Output.NotFound($"No user with username {context.Input.Username} found");
            } else {
                context.Output.Success(user);
            }
        }
        #endregion
    }
}
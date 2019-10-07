using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQueryHandler : QueryHandler<IsUsernameAvailableQuery> {
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
        protected async override Task ExecuteQuery(ExecutionContext<IsUsernameAvailableQuery> context) {
            UserReadView? user = await userReader.FindByUsername(context.Input.Username);

            if (user == null) {
                context.Output.NotFound($"No user with username {context.Input.Username} found");
            } else {
                context.Output.Success();
            }
        }
        #endregion
    }
}
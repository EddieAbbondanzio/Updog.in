using Updog.Domain;

namespace Updog.Application {
    public sealed class LoginUserCommand : ICommand {
        #region Properties
        public UserCredentials Credentials { get; }
        #endregion

        #region Constructor(s)
        public LoginUserCommand(UserCredentials credentials) {
            Credentials = credentials;
        }
        #endregion

    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class LoginUserCommand : AnonymousCommand {
        #region Properties
        public UserCredentials Credentials { get; set; } = null!;
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public abstract class UserAlterCommand : AuthenticatedCommand {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public UserAlterCommand(string username, User user) : base(user) {
            Username = username;
        }
        #endregion
    }
}
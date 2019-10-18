using Updog.Domain;

namespace Updog.Application {
    public abstract class RoleAlterCommand : AuthenticatedCommand {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public RoleAlterCommand(string username, User user) : base(user) {
            Username = username;
        }
        #endregion
    }
}
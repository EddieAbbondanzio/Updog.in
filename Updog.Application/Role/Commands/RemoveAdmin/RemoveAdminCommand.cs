using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveAdminCommand : AuthenticatedCommand {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public RemoveAdminCommand(string username, User user) : base(user) {
            Username = username;
        }
        #endregion
    }
}
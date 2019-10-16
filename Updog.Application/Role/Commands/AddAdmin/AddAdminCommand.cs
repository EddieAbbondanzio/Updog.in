using Updog.Domain;

namespace Updog.Application {
    public sealed class AddAdminCommand : AuthenticatedCommand {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public AddAdminCommand(string username, User user) : base(user) {
            Username = username;
        }
        #endregion
    }
}
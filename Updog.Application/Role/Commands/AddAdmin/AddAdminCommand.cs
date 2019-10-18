using Updog.Domain;

namespace Updog.Application {
    public sealed class AddAdminCommand : RoleAlterCommand {
        #region Constructor(s)
        public AddAdminCommand(string username, User user) : base(username, user) {
        }
        #endregion
    }
}
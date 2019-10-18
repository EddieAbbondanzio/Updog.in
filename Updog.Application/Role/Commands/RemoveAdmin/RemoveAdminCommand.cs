using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveAdminCommand : RoleAlterCommand {
        #region Constructor(s)
        public RemoveAdminCommand(string username, User user) : base(username, user) {
        }
        #endregion
    }
}
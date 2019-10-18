
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddModeratorToSpaceCommand : RoleAlterCommand {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public AddModeratorToSpaceCommand(string space, string username, User user) : base(username, user) {
            Space = space;
        }
        #endregion
    }
}
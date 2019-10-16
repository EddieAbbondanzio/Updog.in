
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddModeratorToSpaceCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public AddModeratorToSpaceCommand(string space, string username, User user) : base(user) {
            Space = space;
            Username = username;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveModeratorFromSpaceCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public RemoveModeratorFromSpaceCommand(string space, string username, User user) : base(user) {
            Space = space;
            Username = username;
        }
        #endregion
    }
}
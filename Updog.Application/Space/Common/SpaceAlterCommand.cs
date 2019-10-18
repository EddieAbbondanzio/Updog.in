using Updog.Domain;

namespace Updog.Application {
    public abstract class SpaceAlterCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SpaceAlterCommand(string space, User user) : base(user) {
            Space = space;
        }
        #endregion
    }
}
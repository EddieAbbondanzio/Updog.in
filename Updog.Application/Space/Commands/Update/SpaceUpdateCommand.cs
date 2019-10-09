using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        public SpaceUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommand(string space, SpaceUpdate update, User user) : base(user) {
            Space = space;
            Update = update;
        }
        #endregion
    }
}
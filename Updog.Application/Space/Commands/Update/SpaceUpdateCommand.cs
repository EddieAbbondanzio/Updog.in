using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateCommand : SpaceAlterCommand {
        #region Properties
        public SpaceUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommand(string space, SpaceUpdate update, User user) : base(space, user) {
            Update = update;
        }
        #endregion
    }
}
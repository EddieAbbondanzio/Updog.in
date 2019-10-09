using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateCommand : AuthenticatedCommand {
        #region Properties
        public SpaceUpdateData Data { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommand(SpaceUpdateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : AuthenticatedCommand {
        #region Properties
        public SpaceCreateData Data { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateCommand(SpaceCreateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
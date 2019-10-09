using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : AuthenticatedCommand {
        #region Properties
        public SpaceCreate Data { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateCommand(SpaceCreate data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : AuthenticatedCommand {
        #region Properties
        public SpaceCreateData CreationData { get; set; } = null!;
        #endregion
    }
}
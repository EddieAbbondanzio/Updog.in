using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : AuthenticatedCommand {
        #region Properties
        public SpaceCreationData CreationData { get; set; } = null!;
        #endregion
    }
}
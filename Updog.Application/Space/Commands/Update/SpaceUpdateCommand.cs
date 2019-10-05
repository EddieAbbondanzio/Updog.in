using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateCommand : AuthenticatedCommand {
        #region Properties
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        #endregion
    }
}
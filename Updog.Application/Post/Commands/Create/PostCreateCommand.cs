using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateCommand : AuthenticatedCommand {
        #region Properties
        public PostCreationData CreationData { get; set; } = null!;
        #endregion
    }
}
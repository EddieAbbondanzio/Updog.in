using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : AuthenticatedCommand {
        #region Properties
        public CommentCreationData CreationData { get; set; } = null!;
        #endregion

    }
}
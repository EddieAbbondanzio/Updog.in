using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : AuthenticatedCommand {
        #region Properties
        public CommentCreateData CreationData { get; set; } = null!;
        #endregion

    }
}
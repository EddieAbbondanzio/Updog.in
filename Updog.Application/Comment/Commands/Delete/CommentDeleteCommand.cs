using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; set; }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteCommand(int commentId, User user) : base(user) {
            CommentId = commentId;
        }
        #endregion
    }
}
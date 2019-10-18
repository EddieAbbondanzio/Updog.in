using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteCommand : CommentAlterCommand {
        #region Constructor(s)
        public CommentDeleteCommand(int commentId, User user) : base(commentId, user) {
        }
        #endregion
    }
}
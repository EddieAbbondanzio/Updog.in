using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateCommand : CommentAlterCommand {
        #region Properties
        public CommentUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateCommand(int commentId, CommentUpdate update, User user) : base(commentId, user) {
            Update = update;
        }
        #endregion
    }
}
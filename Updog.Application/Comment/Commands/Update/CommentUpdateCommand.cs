using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; }
        public CommentUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateCommand(int commentId, CommentUpdate update, User user) : base(user) {
            CommentId = commentId;
            Update = update;
        }
        #endregion
    }
}
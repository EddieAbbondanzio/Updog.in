using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteParams {
        #region Properties
        public User User { get; }

        public int CommentId { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteParams(User user, int comment) {
            User = user;
            CommentId = comment;
        }
        #endregion
    }
}
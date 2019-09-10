using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteParams : IAuthenticatedActionParams {
        #region Properties
        public int CommentId { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteParams(User user, int comment) {
            User = user;
            CommentId = comment;
        }
        #endregion
    }
}
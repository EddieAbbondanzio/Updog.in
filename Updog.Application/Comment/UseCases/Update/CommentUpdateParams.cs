using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateParams {
        #region Properties
        public int CommentId { get; }

        public string Body { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateParams(User user, int commentId, string body) {
            User = user;
            CommentId = commentId;
            Body = body;
        }
        #endregion
    }
}
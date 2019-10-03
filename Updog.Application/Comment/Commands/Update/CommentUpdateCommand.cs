using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateCommand : ICommand {
        #region Properties
        public User User { get; }

        public int CommentId { get; }

        public string Body { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateCommand(User user, int commentId, string body) {
            User = user;
            CommentId = commentId;
            Body = body;
        }
        #endregion
    }
}
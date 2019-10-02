using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommand : ICommand {
        #region Properties
        public User User { get; }
        public int CommentId { get; }
        public VoteDirection Vote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentCommand(User user, int commentId, VoteDirection vote) {
            User = user;
            CommentId = commentId;
            Vote = vote;
        }
        #endregion
    }
}
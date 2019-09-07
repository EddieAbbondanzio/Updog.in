using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to vote on a comment.
    /// </summary>
    public sealed class VoteOnCommentParams {
        #region Properties
        /// <summary>
        /// The comment ID to vote on.
        /// </summary>
        /// <value></value>
        public int CommentId { get; }

        /// <summary>
        /// What type of vote it is.
        /// </summary>
        public VoteDirection Vote { get; }

        /// <summary>
        /// The user perofrming the action.
        /// </summary>
        /// <value></value>
        public User User { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentParams(int commentId, VoteDirection vote, User user) {
            CommentId = commentId;
            Vote = vote;
            User = user;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to vote on a post.
    /// </summary>
    public sealed class VoteOnPostParams {
        #region Properties
        /// <summary>
        /// The post ID to vote on.
        /// </summary>
        /// <value></value>
        public int PostId { get; }

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
        public VoteOnPostParams(int postId, VoteDirection vote, User user) {
            PostId = postId;
            Vote = vote;
            User = user;
        }
        #endregion
    }
}
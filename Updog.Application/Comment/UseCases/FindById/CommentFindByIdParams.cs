using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find a comment by it's unique ID.
    /// </summary>
    public sealed class CommentFindByIdParams : IAnonymousActionParams {
        #region Properties
        /// <summary>
        /// The ID of the comment to look for.
        /// </summary>
        public int CommentId { get; }

        /// <summary>
        /// The user performing the look up.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public CommentFindByIdParams(int commentId, User? user = null) {
            CommentId = commentId;
            User = user;
        }
        #endregion
    }
}
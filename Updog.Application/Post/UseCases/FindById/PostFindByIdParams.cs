using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find a post by its unique ID.
    /// </summary>
    public sealed class PostFindByIdParams : IAnonymousActionParams {
        #region Properties
        /// <summary>
        /// The ID of the post to look for.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public PostFindByIdParams(int postId, User? user = null) {
            PostId = postId;
            User = user;
        }
        #endregion
    }
}
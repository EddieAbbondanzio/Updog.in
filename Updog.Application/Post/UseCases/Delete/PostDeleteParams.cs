using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteParams {
        #region Properties
        /// <summary>
        /// The user that wants to delete the post.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// The ID of the post to delete.
        /// </summary>
        /// <value></value>
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of post delete params.
        /// </summary>
        /// <param name="user">The uer doing it.</param>
        /// <param name="postId">The ID of the post.</param>
        public PostDeleteParams(User user, int postId) {
            User = user;
            PostId = postId;
        }
        #endregion
    }
}
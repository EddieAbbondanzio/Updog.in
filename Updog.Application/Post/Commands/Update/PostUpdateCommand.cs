using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateCommand : ICommand {
        #region Properties
        /// <summary>
        /// The user doing the update.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// The ID of the post to update.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The new text of the post.
        /// </summary>
        public string Body { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of update Command for a post.
        /// </summary>
        /// <param name="user">The user updating the post.</param>
        /// <param name="postId">The post ID.</param>
        /// <param name="body">The text of the post.</param>
        public PostUpdateCommand(User user, int postId, string body) {
            User = user;
            PostId = postId;
            Body = body;
        }
        #endregion
    }
}
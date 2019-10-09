using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteCommand : AuthenticatedCommand {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteCommand(int postId, User user) : base(user) {
            PostId = postId;
        }
        #endregion
    }
}
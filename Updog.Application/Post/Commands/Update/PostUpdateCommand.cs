using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateCommand : AuthenticatedCommand {
        #region Properties
        public int PostId { get; }
        public PostUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateCommand(int postId, PostUpdate update, User user) : base(user) {
            PostId = postId;
            Update = update;
        }
        #endregion
    }
}
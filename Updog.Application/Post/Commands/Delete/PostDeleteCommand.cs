using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteCommand : PostAlterCommand {
        #region Constructor(s)
        public PostDeleteCommand(int postId, User user) : base(postId, user) {
        }
        #endregion
    }
}
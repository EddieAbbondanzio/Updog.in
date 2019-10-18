using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateCommand : PostAlterCommand {
        #region Properties
        public PostUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateCommand(int postId, PostUpdate update, User user) : base(postId, user) {
            Update = update;
        }
        #endregion
    }
}
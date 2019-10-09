using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteCommand : AuthenticatedCommand {
        #region Properties
        /// <summary>
        /// The ID of the post to delete.
        /// </summary>
        /// <value></value>
        public PostDeleteData Data { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteCommand(PostDeleteData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
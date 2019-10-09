using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateCommand : AuthenticatedCommand {
        #region Properties
        public PostUpdateData Data { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateCommand(PostUpdateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
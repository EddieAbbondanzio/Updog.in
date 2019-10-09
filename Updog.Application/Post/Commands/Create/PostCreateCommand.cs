using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateCommand : AuthenticatedCommand {
        #region Properties
        public PostCreateData Data { get; }
        #endregion

        #region Constructor(s)
        public PostCreateCommand(PostCreateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
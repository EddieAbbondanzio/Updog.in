using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        public PostCreate Data { get; }
        #endregion

        #region Constructor(s)
        public PostCreateCommand(string space, PostCreate data, User user) : base(user) {
            Space = space;
            Data = data;
        }
        #endregion
    }
}
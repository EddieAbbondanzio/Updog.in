using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateCommand : ICommand {
        #region Properties
        public PostCreationData CreationData { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public PostCreateCommand(PostCreationData creationData, User user) {
            CreationData = creationData;
            User = user;
        }
        #endregion
    }
}
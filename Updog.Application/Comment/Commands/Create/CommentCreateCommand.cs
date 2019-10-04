using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : ICommand {
        #region Properties
        public CommentCreationData CreationData { get; }

        /// <summary>
        /// The user that is creating the comment.
        /// </summary>
        public User User { get; }
        #endregion

        #region Constructor(s)
        public CommentCreateCommand(CommentCreationData creationData, User user) {
            CreationData = creationData;
            User = user;
        }
        #endregion
    }
}
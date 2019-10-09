using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a comment.
    /// </summary>
    public sealed class CommentDeleteCommand : AuthenticatedCommand {
        #region Properties
        public CommentDeleteData Data { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteCommand(CommentDeleteData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
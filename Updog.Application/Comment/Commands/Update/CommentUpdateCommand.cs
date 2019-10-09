using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateCommand : AuthenticatedCommand {
        #region Properties
        public CommentUpdateData Data { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateCommand(CommentUpdateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
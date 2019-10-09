using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : AuthenticatedCommand {
        #region Properties
        public CommentCreateData Data { get; }
        #endregion

        #region Constructor(s)
        public CommentCreateCommand(CommentCreateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : AuthenticatedCommand {
        #region Properties
        public CommentCreate Data { get; }
        #endregion

        #region Constructor(s)
        public CommentCreateCommand(CommentCreate data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {

    public abstract class CommentAlterCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; }
        #endregion

        #region Constructor(s)
        public CommentAlterCommand(int commentId, User user) : base(user) {
            CommentId = commentId;
        }
        #endregion
    }
}
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQuery : IQuery {
        #region Properties
        public int CommentId { get; }

        public User? User { get; }
        #endregion

        #region Constructor(s)
        public CommentFindByIdQuery(int commentId, User? user = null) {
            CommentId = commentId;
            User = user;
        }
        #endregion
    }
}
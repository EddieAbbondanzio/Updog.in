using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQuery : AnonymousQuery {
        #region Properties
        public int CommentId { get; }
        #endregion

        #region Constructor(s)
        public CommentFindByIdQuery(int commentId, User user) : base(user) {
            CommentId = commentId;
        }
        #endregion
    }
}
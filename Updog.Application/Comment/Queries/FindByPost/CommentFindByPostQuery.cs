using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByPostQuery : AnonymousQuery {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public CommentFindByPostQuery(int postId, User user) : base(user) {
            PostId = postId;
        }
        #endregion
    }
}
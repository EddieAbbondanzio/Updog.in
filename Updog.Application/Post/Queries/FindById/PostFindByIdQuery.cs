using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQuery : AnonymousQuery {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public PostFindByIdQuery(int postId, User? user = null) : base(user) {
            PostId = postId;
        }
        #endregion
    }
}
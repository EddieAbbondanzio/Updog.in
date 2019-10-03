using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQuery : IQuery {
        #region Properties
        public int PostId { get; }

        public User? User { get; }
        #endregion

        #region Constructor(s)
        public PostFindByIdQuery(int postId, User? user = null) {
            PostId = postId;
            User = user;
        }
        #endregion
    }
}
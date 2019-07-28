using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteParams {
        #region Properties
        public User User { get; }

        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteParams(User user, int post) {
            User = user;
            PostId = post;
        }
        #endregion
    }
}
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Parameters to delete a post.
    /// </summary>
    public sealed class PostDeleteParams {
        #region Properties
        public User User { get; }

        public Post Post { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteParams(User user, Post post) {
            User = user;
            Post = post;
        }
        #endregion
    }
}
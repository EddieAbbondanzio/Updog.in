using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateParams {
        #region Properties
        public Post Post { get; }

        public string Body { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateParams(User user, Post post, string body) {
            User = user;
            Post = post;
            Body = body;
        }
        #endregion
    }
}
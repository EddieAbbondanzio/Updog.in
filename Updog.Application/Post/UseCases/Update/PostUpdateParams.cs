using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateParams {
        #region Properties
        public int PostId { get; }

        public string Body { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateParams(User user, int postId, string body) {
            User = user;
            PostId = postId;
            Body = body;
        }
        #endregion
    }
}
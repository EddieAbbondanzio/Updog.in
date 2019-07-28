using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateParams {
        #region Properties
        public PostType Type { get; }

        public string Title { get; }

        public string Body { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public PostCreateParams(PostType type, string title, string body, User user) {
            Type = type;
            Title = title;
            Body = body;
            User = user;
        }
        #endregion
    }
}
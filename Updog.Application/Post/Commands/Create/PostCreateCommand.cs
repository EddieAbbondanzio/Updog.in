using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to add a new post.
    /// </summary>
    public sealed class PostCreateCommand : ICommand {
        #region Properties
        public PostType Type { get; }

        public string Title { get; }

        public string Body { get; }

        public string Space { get; }

        public User User { get; }

        #endregion

        #region Constructor(s)
        public PostCreateCommand(PostType type, string title, string body, string space, User user) {
            Type = type;
            Title = title;
            Body = body;
            Space = space;
            User = user;
        }
        #endregion
    }
}
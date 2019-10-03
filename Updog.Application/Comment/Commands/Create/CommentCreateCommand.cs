using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommand : ICommand {
        #region Properties
        /// <summary>
        /// The ID of the post it belongs to.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The ID of the parent comment.
        /// </summary>
        public int ParentId { get; }

        /// <summary>
        /// The user that is creating the comment.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of comment create parameters.
        /// </summary>
        /// <param name="postId">The post it is on.</param>
        /// <param name="user">The user that made the post.</param>
        /// <param name="body">The text body of the comment.</param>
        /// <param name="parentId">The parent comment</param>
        public CommentCreateCommand(int postId, User user, string body, int parentId = 0) {
            PostId = postId;
            Body = body;
            User = user;
            ParentId = parentId;
        }
        #endregion
    }
}
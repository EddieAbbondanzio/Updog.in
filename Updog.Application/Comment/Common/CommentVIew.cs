namespace Updog.Application {
    public sealed class CommentView : IView {
        #region Properties
        /// <summary>
        /// The unique ID of the comment.
        /// </summary>
        /// <value></value>
        public int Id { get; }

        /// <summary>
        /// The user that created the comment.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The parent comment (if any).
        /// </summary>
        public CommentView Parent { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment view.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="user">The user that made the comment.</param>
        /// <param name="body">The text of the comment.</param>
        /// <param name="parent">The parent comment (if any).</param>
        public CommentView(int id, UserView user, string body, CommentView parent = null) {
            Id = id;
            User = user;
            Body = body;
            Parent = parent;
        }
        #endregion
    }
}
namespace Updog.Application {
    /// <summary>
    /// Information about a comment.
    /// </summary>
    public sealed class CommentInfo {
        #region Properties
        /// <summary>
        /// It's unique identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The username of who created the comment.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment info.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="author">The name of the author.</param>
        /// <param name="body">The body of the comment.</param>
        public CommentInfo(int id, string author, string body) {
            Id = id;
            Author = author;
            Body = body;
        }
        #endregion
    }
}
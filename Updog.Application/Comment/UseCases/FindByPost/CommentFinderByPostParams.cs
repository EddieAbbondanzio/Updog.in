namespace Updog.Application {
    /// <summary>
    /// Params to find a page of comments for a post.
    /// </summary>
    public sealed class CommentFinderByPostParams {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of comment finder by post params.
        /// </summary>
        /// <param name="postId">The post ID to look for.</param>
        public CommentFinderByPostParams(int postId) {
            PostId = postId;
        }
        #endregion
    }
}
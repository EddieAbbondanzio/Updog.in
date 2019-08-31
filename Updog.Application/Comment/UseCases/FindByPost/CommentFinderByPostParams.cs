namespace Updog.Application {
    /// <summary>
    /// Params to find a page of comments for a post.
    /// </summary>
    public sealed class CommentFinderByPostParams : IPagable {
        #region Properties
        public int PostId { get; }

        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of comment finder by post params.
        /// </summary>
        /// <param name="postId">The post ID to look for.</param>
        /// <param name="pageNumber">Page index</param>
        /// <param name="pageSize">Page size</param>
        public CommentFinderByPostParams(int postId, int pageNumber, int pageSize) {
            PostId = postId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}
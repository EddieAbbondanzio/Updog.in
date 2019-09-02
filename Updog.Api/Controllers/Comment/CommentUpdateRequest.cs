namespace Updog.Api {
    /// <summary>
    /// Request payload to update a comment.
    /// </summary>
    public sealed class CommentUpdateRequest {
        #region Properties
        /// <summary>
        /// The new body of the post.
        /// </summary>
        public string Body { get; set; }
        #endregion
    }
}
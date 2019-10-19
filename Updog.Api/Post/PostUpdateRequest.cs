namespace Updog.Api {
    /// <summary>
    /// Request payload to update a post.
    /// </summary>
    public sealed class PostUpdateRequest {
        #region Properties
        /// <summary>
        /// The new body of the post.
        /// </summary>
        public string Body { get; set; } = "";
        #endregion
    }
}
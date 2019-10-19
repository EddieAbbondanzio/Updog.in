using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Request to create a new comment.
    /// </summary>
    public sealed class CommentCreateRequest {
        #region Properties
        /// <summary>
        /// The ID of the post it belongs to.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// The ID of the parent comment (if any).
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; } = "";
        #endregion
    }
}
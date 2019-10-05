using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a post.
    /// </summary>
    public sealed class PostUpdateCommand : AuthenticatedCommand {
        #region Properties
        /// <summary>
        /// The ID of the post to update.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// The new text of the post.
        /// </summary>
        public string Body { get; set; } = "";
        #endregion
    }
}
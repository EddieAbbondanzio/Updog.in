using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a comment.
    /// </summary>
    public sealed class CommentUpdateCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; set; }
        public string Body { get; set; } = "";
        #endregion
    }
}
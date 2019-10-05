using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommand : AuthenticatedCommand {
        #region Properties
        public int CommentId { get; set; }
        public VoteDirection Vote { get; set; } = VoteDirection.Neutral;
        #endregion
    }
}
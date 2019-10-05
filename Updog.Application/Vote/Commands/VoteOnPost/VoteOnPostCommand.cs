using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommand : AuthenticatedCommand {
        #region Properties
        public int PostId { get; set; }
        public VoteDirection Vote { get; set; } = VoteDirection.Neutral;
        #endregion
    }
}
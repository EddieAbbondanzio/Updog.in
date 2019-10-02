using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommand : ICommand {
        #region Properties
        public User User { get; }
        public int PostId { get; }
        public VoteDirection Vote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostCommand(User user, int postId, VoteDirection vote) {
            User = user;
            PostId = postId;
            Vote = vote;
        }
        #endregion
    }
}
namespace Updog.Domain {
    public sealed class VoteOnPostEvent : IDomainEvent {
        #region Properties
        public int PostId { get; }
        public Vote NewVote { get; }
        public Vote? OldVote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostEvent(int postId, Vote newVote, Vote? oldVote = null) {
            PostId = postId;
            OldVote = oldVote;
            NewVote = newVote;
        }
        #endregion
    }
}
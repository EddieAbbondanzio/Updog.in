namespace Updog.Domain {
    public sealed class VoteOnCommentEvent : IDomainEvent {
        #region Properties
        public int CommentId { get; }
        public Vote NewVote { get; }
        public Vote? OldVote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentEvent(int commentId, Vote newVote, Vote? oldVote = null) {
            CommentId = commentId;
            NewVote = newVote;
            OldVote = oldVote;
        }
        #endregion
    }
}
namespace Updog.Domain {
    public sealed class VoteOnCommentEvent : IDomainEvent {
        #region Properties
        public Vote Vote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentEvent(Vote v) {
            Vote = v;
        }
        #endregion
    }
}
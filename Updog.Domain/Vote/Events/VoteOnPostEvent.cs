namespace Updog.Domain {
    public sealed class VoteOnPostEvent : IDomainEvent {
        #region Properties
        public Vote Vote { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostEvent(Vote v) {
            Vote = v;
        }
        #endregion
    }
}
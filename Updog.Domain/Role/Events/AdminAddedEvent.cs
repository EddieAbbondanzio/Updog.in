namespace Updog.Domain {
    public sealed class AdminAddedEvent : IDomainEvent {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public AdminAddedEvent(User user) {
            User = user;
        }
        #endregion
    }
}
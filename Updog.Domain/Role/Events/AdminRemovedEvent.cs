namespace Updog.Domain {
    public sealed class AdminRemovedEvent : IDomainEvent {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public AdminRemovedEvent(User user) {
            User = user;
        }
        #endregion
    }
}
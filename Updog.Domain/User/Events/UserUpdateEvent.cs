namespace Updog.Domain {
    public sealed class UserUpdateEvent : IDomainEvent {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateEvent(User user) {
            User = user;
        }
        #endregion
    }
}
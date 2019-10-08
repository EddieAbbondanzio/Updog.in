namespace Updog.Domain {
    public sealed class UserUpdatePasswordEvent : IDomainEvent {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordEvent(User user) {
            User = user;
        }
        #endregion
    }
}
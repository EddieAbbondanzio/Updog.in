namespace Updog.Domain {
    public sealed class UserLoginEvent : IDomainEvent {
        #region Properties
        public User User { get; }

        public UserLogin Login { get; }
        #endregion

        #region Constructor(s)
        public UserLoginEvent(User user, UserLogin login) {
            User = user;
            Login = login;
        }
        #endregion
    }
}
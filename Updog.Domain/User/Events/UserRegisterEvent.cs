namespace Updog.Domain {
    public sealed class UserRegisterEvent : IDomainEvent {
        #region Properties
        public User User { get; }

        public UserLogin Login { get; }
        #endregion

        #region Constructor(s)
        public UserRegisterEvent(User user, UserLogin login) {
            User = user;
            Login = login;
        }
        #endregion
    }
}
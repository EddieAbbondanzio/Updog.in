namespace Updog.Domain {
    public sealed class UserUpdateData : IValueObject {
        #region Properties
        public User User { get; }

        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateData(User user, string email) {
            User = user;
            Email = email;
        }
        #endregion
    }
}
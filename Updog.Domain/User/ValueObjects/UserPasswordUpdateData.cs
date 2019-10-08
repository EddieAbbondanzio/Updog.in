namespace Updog.Domain {
    public sealed class UserPasswordUpdateData : IValueObject {
        #region Properties
        public User User { get; }

        public string CurrentPassword { get; }

        public string NewPassword { get; }
        #endregion

        #region Constructor(s)
        public UserPasswordUpdateData(User user, string currentPassword, string newPassword) {
            User = user;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        #endregion
    }
}
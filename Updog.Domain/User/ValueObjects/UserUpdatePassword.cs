namespace Updog.Domain {
    public sealed class UserUpdatePassword : IValueObject {
        #region Properties
        public string CurrentPassword { get; }

        public string NewPassword { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePassword(string currentPassword, string newPassword) {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        #endregion
    }
}
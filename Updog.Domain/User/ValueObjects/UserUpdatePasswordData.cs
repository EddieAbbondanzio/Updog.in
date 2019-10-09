namespace Updog.Domain {
    public sealed class UserUpdatePasswordData : IValueObject {
        #region Properties
        public string CurrentPassword { get; }

        public string NewPassword { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordData(string currentPassword, string newPassword) {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        #endregion
    }
}
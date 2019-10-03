using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a user's password.
    /// </summary>
    public sealed class UserUpdatePasswordCommand : ICommand {
        #region Properties
        /// <summary>
        /// The user that wants to update their password.
        /// </summary>
        /// <value></value>
        public User User { get; }

        /// <summary>
        /// The old password they already have in use.
        /// </summary>
        /// <value></value>
        public string CurrentPassword { get; }

        /// <summary>
        /// The new password they want to use.
        /// </summary>
        public string NewPassword { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordCommand(User user, string currentPassword, string newPassword) {
            User = user;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        #endregion
    }
}
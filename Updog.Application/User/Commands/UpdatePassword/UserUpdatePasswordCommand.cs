using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a user's password.
    /// </summary>
    public sealed class UserUpdatePasswordCommand : UserAlterCommand {
        #region Properties
        public UserUpdatePassword UpdatePassword { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordCommand(string username, UserUpdatePassword data, User user) : base(username, user) {
            UpdatePassword = data;
        }
        #endregion
    }
}
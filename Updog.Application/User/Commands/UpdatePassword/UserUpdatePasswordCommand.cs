using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a user's password.
    /// </summary>
    public sealed class UserUpdatePasswordCommand : AuthenticatedCommand {
        #region Properties
        /// <summary>
        /// The old password they already have in use.
        /// </summary>
        /// <value></value>
        public string CurrentPassword { get; set; } = "";

        /// <summary>
        /// The new password they want to use.
        /// </summary>
        public string NewPassword { get; set; } = "";
        #endregion
    }
}
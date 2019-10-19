namespace Updog.Api {
    /// <summary>
    /// Request in the "Me" controller to update a password.
    /// </summary>
    public sealed class MeUpdatePasswordRequest {
        #region Properties
        /// <summary>
        /// The current password the user is using.
        /// </summary>
        /// <value></value>
        public string CurrentPassword { get; set; } = "";

        /// <summary>
        /// The new password the user wants to use.
        /// </summary>
        /// <value></value>
        public string NewPassword { get; set; } = "";
        #endregion
    }
}
namespace Blurtle.Application {
    /// <summary>
    /// Validation result from a auth bearer token being validated.
    /// </summary>
    public sealed class AuthenticationTokenValidationResult {
        #region Properties
        /// <summary>
        /// If the token was valid or not.
        /// </summary>
        /// <value></value>
        public AuthenticationTokenStatus Status { get; }

        /// <summary>
        /// The ID of the user it belongs to.
        /// </summary>
        public int UserId { get; }
        #endregion

        #region Constructor(s)
        public AuthenticationTokenValidationResult(AuthenticationTokenStatus status) {
            Status = status;
        }

        public AuthenticationTokenValidationResult(AuthenticationTokenStatus status, int userId) {
            Status = status;
            UserId = userId;
        }
        #endregion
    }
}
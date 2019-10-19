namespace Updog.Api {
    /// <summary>
    /// Body of a request to log in and start a new session
    /// with the server.
    /// </summary>
    public sealed class SessionLoginRequest {
        #region Publics
        /// <summary>
        /// The username to log in under.
        /// </summary>
        /// <value></value>
        public string Username { get; set; } = "";

        /// <summary>
        /// The password to authenticate with.
        /// </summary>
        public string Password { get; set; } = "";
        #endregion
    }
}
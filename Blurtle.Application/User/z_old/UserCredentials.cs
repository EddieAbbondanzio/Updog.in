namespace Blurtle.Application {
    /// <summary>
    /// Credentials to log in a user.
    /// </summary>
    public sealed class UserCredentials {
        #region Properties
        public string Username { get; set; }

        public string Password { get; set; }
        #endregion
    }
}
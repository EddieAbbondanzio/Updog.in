namespace Blurtle.Application {
    /// <summary>
    /// Registration info for a new user.
    /// </summary>
    public sealed class UserRegistration {
        #region Properties
        /// <summary>
        /// The username they want to use.
        /// </summary>
        /// <value></value>
        public string Username { get; }

        /// <summary>
        /// The password the user wants to use.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The email of the user (if any).
        /// </summary>
        public string Email { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user registration.
        /// </summary>
        /// <param name="username">The username they want.</param>
        /// <param name="password">The password they want to use.</param>
        /// <param name="email">The contact email (if any).</param>
        public UserRegistration(string username, string password, string email = null) {
            Username = username;
            Password = password;
            Email = email;
        }
        #endregion
    }
}
namespace Updog.Domain {
    public sealed class UserRegistration : IValueObject {
        #region Properties
        /// <summary>
        /// The username they want.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password they want to use.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The contact email (if any).
        /// </summary>
        public string? Email { get; }
        #endregion

        #region 
        public UserRegistration(string username, string password, string? email) {
            Username = username;
            Password = password;
            Email = email;
        }
        #endregion
    }
}
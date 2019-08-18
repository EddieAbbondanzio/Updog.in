using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// User login record and the issued auth token.
    /// </summary>
    public sealed class UserLogin {
        #region Properties
        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The auth token they were issued.
        /// </summary>
        /// <value></value>
        public string AuthToken { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create  new user login.
        /// </summary>
        /// <param name="user">The user of the login.</param>
        /// <param name="authToken">Their JWT.</param>
        public UserLogin(UserView user, string authToken) {
            User = user;
            AuthToken = authToken;
        }
        #endregion
    }
}
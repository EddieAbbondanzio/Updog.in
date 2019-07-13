using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// User login record and the issued auth token.
    /// </summary>
    public sealed class UserLogin {
        #region Properties
        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public UserInfo User { get; }

        /// <summary>
        /// The auth token they were issued.
        /// </summary>
        /// <value></value>
        public string AuthToken { get; }
        #endregion

        #region Constructor(s)
        public UserLogin(UserInfo user, string authToken) {
            User = user;
            AuthToken = authToken;
        }
        #endregion
    }
}
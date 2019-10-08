using Updog.Domain;

namespace Updog.Domain {
    /// <summary>
    /// User login record and the issued auth token.
    /// </summary>
    public sealed class UserLogin : IValueObject {
        #region Properties
        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The auth token they were issued.
        /// </summary>
        /// <value></value>
        public string AuthToken { get; }
        #endregion

        #region Constructor(s)
        public UserLogin(int userId, string authToken) {
            UserId = userId;
            AuthToken = authToken;
        }
        #endregion
    }
}
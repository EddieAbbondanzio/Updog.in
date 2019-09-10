using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find a user via their username.
    /// </summary>
    public sealed class UserFindByUsernameParams : IAnonymousActionParams {
        #region Properties
        /// <summary>
        /// The username to look for.
        /// </summary>
        /// <value></value>
        public string Username { get; }

        /// <summary>
        /// The user peforming the action.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public UserFindByUsernameParams(string username, User? user = null) {
            Username = username;
            User = user;
        }
        #endregion
    }
}
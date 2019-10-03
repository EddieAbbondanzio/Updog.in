using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQuery : IQuery {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public IsUsernameAvailableQuery(string username) {
            Username = username;
        }
        #endregion
    }
}
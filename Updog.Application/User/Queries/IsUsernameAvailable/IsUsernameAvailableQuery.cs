using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQuery : AnonymousQuery {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public IsUsernameAvailableQuery(string username, User? user = null) : base(user) {
            Username = username;
        }
        #endregion
    }
}
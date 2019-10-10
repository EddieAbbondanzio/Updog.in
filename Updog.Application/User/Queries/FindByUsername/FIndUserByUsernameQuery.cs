using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQuery : AnonymousQuery {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public FindUserByUsernameQuery(string username, User? user = null) : base(user) {
            Username = username;
        }
        #endregion
    }
}
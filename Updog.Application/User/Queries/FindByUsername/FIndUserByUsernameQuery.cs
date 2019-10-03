using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQuery : IQuery {
        #region Properties
        public User? User { get; }

        public string Username { get; }
        #endregion

        #region Constructor(s)
        public FindUserByUsernameQuery(string username, User? user) {
            Username = username;
            User = user;
        }
        #endregion
    }
}
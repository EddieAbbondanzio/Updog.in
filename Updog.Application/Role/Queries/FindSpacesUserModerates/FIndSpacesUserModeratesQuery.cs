using Updog.Domain;

namespace Updog.Application {
    public sealed class FindSpacesUserModeratesQuery : AnonymousQuery {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public FindSpacesUserModeratesQuery(string username, User? user = null) : base(user) {
            Username = username;
        }
        #endregion
    }
}
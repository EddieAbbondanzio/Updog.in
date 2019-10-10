using Updog.Domain;

namespace Updog.Application {
    public abstract class AuthenticatedQuery : IQuery {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public AuthenticatedQuery(User user) {
            User = user;
        }
        #endregion
    }
}
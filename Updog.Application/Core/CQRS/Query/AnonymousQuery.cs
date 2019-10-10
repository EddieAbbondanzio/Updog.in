using Updog.Domain;

namespace Updog.Application {
    public abstract class AnonymousQuery : IQuery {
        #region Properties
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public AnonymousQuery(User? user = null) {
            User = user;
        }
        #endregion
    }
}
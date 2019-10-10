
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscribedSpaceQuery : AuthenticatedQuery {
        #region Constructor(s)
        public SubscribedSpaceQuery(User user) : base(user) { }
        #endregion
    }
}
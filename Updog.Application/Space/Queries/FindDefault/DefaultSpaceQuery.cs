
using Updog.Domain;

namespace Updog.Application {
    public sealed class DefaultSpaceQuery : AnonymousQuery {
        #region Constructor(s)
        public DefaultSpaceQuery(User? user = null) : base(user) { }
        #endregion
    }
}
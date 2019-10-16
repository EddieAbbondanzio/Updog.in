using Updog.Domain;

namespace Updog.Application {
    public sealed class FindAdminsQuery : AnonymousQuery {
        #region Constructor(s)
        public FindAdminsQuery(User? user) : base(user) { }
        #endregion
    }
}
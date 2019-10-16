using Updog.Domain;

namespace Updog.Application {
    public sealed class FindModeratorsBySpaceQuery : AnonymousQuery {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public FindModeratorsBySpaceQuery(string space, User? user = null) : base(user) {
            Space = space;
        }
        #endregion
    }
}
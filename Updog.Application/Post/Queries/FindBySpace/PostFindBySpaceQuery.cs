using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindBySpaceQuery : IQuery {
        #region Properties
        public string Space { get; }

        public User? User { get; }

        public Paging.PaginationInfo? Paging { get; }
        #endregion
        #region Constructor(s)
        public PostFindBySpaceQuery(string space, User? user = null, Paging.PaginationInfo? paging = null) {
            Space = space;
            User = user;
            Paging = paging;
        }
        #endregion
    }
}
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindBySpaceQuery : IQuery {
        #region Properties
        public string Space { get; }

        public User? User { get; }

        public PaginationInfo? Paging { get; }
        #endregion
        #region Constructor(s)
        public PostFindBySpaceQuery(string space, User? user = null, PaginationInfo? paging = null) {
            Space = space;
            User = user;
            Paging = paging;
        }
        #endregion
    }
}
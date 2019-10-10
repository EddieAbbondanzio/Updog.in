using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindBySpaceQuery : AnonymousQuery, IPagableQuery {
        #region Properties
        public string Space { get; }
        public PaginationInfo Paging { get; }
        #endregion

        #region Constructor(s)
        public PostFindBySpaceQuery(string space, PaginationInfo? paging = null, User? user = null) : base(user) {
            Space = space;
            Paging = paging ?? new PaginationInfo(0, Post.PageSize);
        }
        #endregion
    }
}
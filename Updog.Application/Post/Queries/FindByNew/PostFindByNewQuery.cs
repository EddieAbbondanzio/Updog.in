using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindByNewQuery : AnonymousQuery, IPagableQuery {
        #region Properties
        public PaginationInfo Paging { get; }
        #endregion

        #region Constructor(s)
        public PostFindByNewQuery(PaginationInfo? paging = null, User? user = null) : base(user) {
            Paging = paging ?? new PaginationInfo(0, Post.PageSize);
        }
        #endregion
    }
}
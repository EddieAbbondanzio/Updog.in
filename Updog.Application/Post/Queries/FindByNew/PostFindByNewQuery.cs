using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindByNewQuery : AnonymousQuery, IPagableQuery {
        #region Properties
        public PaginationInfo Paging { get; set; } = new PaginationInfo(0, Post.PageSize);
        #endregion
    }
}
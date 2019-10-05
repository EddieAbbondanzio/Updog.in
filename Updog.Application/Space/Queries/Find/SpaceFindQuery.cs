using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class SpaceFindQuery : AnonymousQuery, IPagableQuery {
        #region Properties
        public PaginationInfo Paging { get; set; } = null!;
        #endregion
    }
}
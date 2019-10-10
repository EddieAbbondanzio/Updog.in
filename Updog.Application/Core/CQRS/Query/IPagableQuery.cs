using Updog.Domain.Paging;

namespace Updog.Application {
    public interface IPagableQuery {
        #region Properties
        PaginationInfo Paging { get; }
        #endregion
    }
}
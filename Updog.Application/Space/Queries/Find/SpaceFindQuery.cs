using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class SpaceFindQuery : IQuery {
        #region Properties
        public PaginationInfo Paging { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindQuery(PaginationInfo paging) {
            Paging = paging;
        }
        #endregion
    }
}
using Updog.Application.Paging;

namespace Updog.Application {
    public sealed class SpaceFindQuery : IQuery {
        #region Properties
        public Paging.PaginationInfo Paging { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindQuery(PaginationInfo paging) {
            Paging = paging;
        }
        #endregion
    }
}
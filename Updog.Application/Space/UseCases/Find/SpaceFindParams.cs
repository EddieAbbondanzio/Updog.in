using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to perform an open find on spaces.
    /// </summary>
    public sealed class SpaceFindParams : IAnonymousActionParams, IPagable {
        #region Properties
        public int PageNumber { get; }

        public int PageSize { get; }

        public User? User { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindParams(int pageNumber, int pageSize, User? user = null) {
            PageNumber = pageNumber;
            PageSize = pageSize;
            User = user;
        }
        #endregion
    }
}
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to perform an open find action.
    /// </summary>
    public class FindParams : IParams, IPagable {
        #region Properties
        /// <summary>
        /// The user performing the find.
        /// </summary>
        public User? User { get; }

        /// <summary>
        /// Paging info.
        /// </summary>
        public PaginationInfo? Pagination { get; }
        #endregion

        #region Constructor(s)
        public FindParams(User? user = null, PaginationInfo? pagination = null) {
            User = user;
            Pagination = pagination;
        }
        #endregion
    }
}
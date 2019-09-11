using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find a post by its unique ID.
    /// </summary>
    public sealed class PostFindByNewParams : IAnonymousActionParams, IPagable {
        #region Properties
        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User? User { get; }

        public int PageSize { get; }

        public int PageNumber { get; }
        #endregion

        #region Constructor(s)
        public PostFindByNewParams(int pageSize, int pageNumber, User? user = null) {
            PageSize = pageSize;
            PageNumber = pageNumber;
            User = user;
        }
        #endregion
    }
}
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find posts by a space.
    /// </summary>
    public sealed class PostFindBySpaceParams : IAnonymousActionParams, IPagable {
        #region Properties
        /// <summary>
        /// The name of the space to look for.
        /// </summary>
        public string Space { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        /// <summary>
        /// The user performing the look up.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public PostFindBySpaceParams(string name, int pageNumber, int pageSize, User? user = null) {
            Space = name;
            PageNumber = pageNumber;
            PageSize = pageSize;
            User = user;
        }
        #endregion
    }
}
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters for the pot finder by user interactor.
    /// </summary>
    public sealed class PostFinderByUserParam : IAnonymousActionParams, IPagable {
        #region Properties
        /// <summary>
        /// The username of the user to look for.
        /// </summary>
        public string Username { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public PostFinderByUserParam(string username, int pageNumber, int pageSize, User? user = null) {
            Username = username;
            PageNumber = pageNumber;
            PageSize = pageSize;
            User = user;
        }
        #endregion
    }
}
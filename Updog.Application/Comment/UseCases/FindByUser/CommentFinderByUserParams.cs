using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters for the comment finder by user interactor.
    /// </summary>
    public sealed class CommentFinderByUserParams : IAnonymousActionParams, IPagable {
        #region Properties
        /// <summary>
        /// The username of the user to look for.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The page number to get.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// The number of comments to return.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// THe user performing the look up.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public CommentFinderByUserParams(string username, int pageNumber, int pageSize, User? user = null) {
            Username = username;
            PageNumber = pageNumber;
            PageSize = pageSize;
            User = user;
        }
        #endregion
    }
}
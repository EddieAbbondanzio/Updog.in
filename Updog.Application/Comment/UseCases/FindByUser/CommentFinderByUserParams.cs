namespace Updog.Application {
    /// <summary>
    /// Parameters for the comment finder by user interactor.
    /// </summary>
    public sealed class CommentFinderByUserParams : IPagable {
        #region Properties
        /// <summary>
        /// The username of the user to look for.
        /// </summary>
        public string Username { get; }

        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of comment finder by user params.
        /// </summary>
        /// <param name="userId">The user ID to look for.</param>
        /// <param name="pageNumber">Page index..</param>
        /// <param name="pageSize">The size of the page..</param>
        public CommentFinderByUserParams(string username, int pageNumber, int pageSize) {
            Username = username;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}
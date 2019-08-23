namespace Updog.Application {
    /// <summary>
    /// Parameters for the comment finder by user interactor.
    /// </summary>
    public sealed class CommentFinderByUserParams {
        #region Properties
        /// <summary>
        /// The username of the user to look for.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// How large of a page, and what page to get.
        /// </summary>
        public PaginationInfo PaginationInfo { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of comment finder by user params.
        /// </summary>
        /// <param name="userId">The user ID to look for.</param>
        /// <param name="paginationInfo">Paging info.</param>
        public CommentFinderByUserParams(string username, PaginationInfo paginationInfo) {
            Username = username;
            PaginationInfo = paginationInfo;
        }
        #endregion
    }
}
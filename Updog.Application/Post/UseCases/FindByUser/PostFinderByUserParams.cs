namespace Updog.Application {
    /// <summary>
    /// Parameters for the pot finder by user interactor.
    /// </summary>
    public sealed class PostFinderByUserParam {
        #region Properties
        /// <summary>
        /// THe ID of the user to look for.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// How large of a page, and what page to get.
        /// </summary>
        public PaginationInfo PaginationInfo { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of post finder by user params.
        /// </summary>
        /// <param name="userId">The user ID to look for.</param>
        /// <param name="paginationInfo">Paging info.</param>
        public PostFinderByUserParam(int userId, PaginationInfo paginationInfo) {
            UserId = userId;
            PaginationInfo = paginationInfo;
        }
        #endregion
    }
}
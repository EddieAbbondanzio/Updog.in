namespace Updog.Application {
    /// <summary>
    /// Pagination info such as page number, and page size.
    /// </summary>
    public sealed class PaginationInfo {
        #region Properties
        /// <summary>
        /// The number of the page.
        /// </summary>
        /// <value></value>
        public int PageNumber { get; }

        /// <summary>
        /// The size of the page.
        /// </summary>
        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new pagination info.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        public PaginationInfo(int pageNumber, int pageSize) {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the row offset of the pagination.
        /// </summary>
        /// <returns>The raw offset of the page result.</returns>
        public int GetOffset() {
            return PageNumber * PageSize;
        }
        #endregion
    }
}
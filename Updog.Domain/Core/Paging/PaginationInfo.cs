using System;

namespace Updog.Domain.Paging {
    /// <summary>
    /// Paging info.
    /// </summary>
    public sealed class PaginationInfo {
        #region Properties
        /// <summary>
        /// The current page number being retrieved.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// The desired page size. (May be smaller if not enough resources
        /// are available to fill all slots)
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// The total number of pages available based on the current page size.
        /// </summary>
        public int TotalRecordCount { get; }

        /// <summary>
        /// The offset of the first record.
        /// </summary>
        public int Offset => PageNumber * PageSize;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of pagination info.
        /// </summary>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="totalRecordCount">Total available pages.</param>
        public PaginationInfo(int pageNumber, int pageSize, int totalRecordCount = 0) {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecordCount = totalRecordCount;
        }
        #endregion
    }
}
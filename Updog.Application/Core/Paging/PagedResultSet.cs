using System.Collections;
using System.Collections.Generic;

namespace Updog.Application.Paging {
    /// <summary>
    /// Result set that had paging applied to it.
    /// </summary>
    /// <typeparam name="T">The resource being paged.</typeparam>
    public sealed class PagedResultSet<T> : IEnumerable<T> {
        #region Properties
        /// <summary>
        /// Pagination info such as page number and page size,
        /// along with total page count.
        /// </summary>
        public PaginationInfo Pagination { get; }

        /// <summary>
        /// The items of the result set.
        /// </summary>
        public IEnumerable<T> Items { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of paged results.
        /// </summary>
        /// <param name="items">The items of the set.</param>
        /// <param name="paginationInfo">The pagination info.</param>
        public PagedResultSet(IEnumerable<T> items, PaginationInfo paginationInfo) {
            Items = items;
            Pagination = paginationInfo;
        }

        public IEnumerator<T> GetEnumerator() {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Items.GetEnumerator();
        }
        #endregion
    }
}
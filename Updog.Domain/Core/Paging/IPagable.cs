namespace Updog.Domain.Paging {
    /// <summary>
    /// Implementation for request params that support paging the results.
    /// </summary>
    public interface IPagable {
        /// <summary>
        /// The pagination info.
        /// </summary>
        PaginationInfo? Pagination { get; }
    }
}
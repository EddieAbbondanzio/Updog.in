namespace Updog.Application.Paging {
    /// <summary>
    /// Implementation for request params that support paging the results.
    /// </summary>
    public interface IPagable {
        /// <summary>
        /// The 0 based index of the page.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// The size of the desired page.
        /// </summary>
        int PageSize { get; }
    }
}
namespace Updog.Application {
    /// <summary>
    /// Parameters for the pot finder by user interactor.
    /// </summary>
    public sealed class PostFinderByNewParams : IPagable {
        #region Properties
        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of post finder by new params.
        /// </summary>
        /// <param name="pageNumber">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PostFinderByNewParams(int pageNumber, int pageSize) {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}
namespace Updog.Application {
    /// <summary>
    /// Parameters to perform an open find on spaces.
    /// </summary>
    public sealed class SpaceFindParams : IPagable {
        #region Properties
        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindParams(int pageNumber, int pageSize) {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}
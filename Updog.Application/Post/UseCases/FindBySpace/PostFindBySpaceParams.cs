using Updog.Application.Paging;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find posts by a space.
    /// </summary>
    public sealed class PostFindBySpaceParams : IPagable {
        #region Properties
        /// <summary>
        /// The name of the space to look for.
        /// </summary>
        public string Space { get; }

        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        public PostFindBySpaceParams(string name, int pageNumber, int pageSize) {
            Space = name;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}
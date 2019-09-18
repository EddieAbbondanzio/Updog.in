using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to perform a find action using a specific value.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class FindByValueParams<TValue> : IParams, IPagable {
        #region Properties
        /// <summary>
        /// The value to search by.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// The user performing the find.
        /// </summary>
        public User? User { get; }

        /// <summary>
        /// Paging info.
        /// </summary>
        public PaginationInfo? Pagination { get; }
        #endregion

        #region Constructor(s)
        public FindByValueParams(TValue value, User? user = null, PaginationInfo? pagination = null) {
            Value = value;
            User = user;
            Pagination = pagination;
        }
        #endregion
    }
}
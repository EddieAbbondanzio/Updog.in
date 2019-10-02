using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Marker interface for query parameters.
    /// </summary>
    public interface IQuery {
        #region Properties
        /// <summary>
        /// User performing the query.
        /// </summary>
        User? User { get; }
        #endregion
    }
}
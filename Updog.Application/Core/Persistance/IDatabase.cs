using System.Data.Common;

namespace Updog.Application {
    /// <summary>
    /// Database for data persistance.
    /// </summary>
    public interface IDatabase {
        #region Publics
        /// <summary>
        /// Get a new pooled connection to the database.
        /// </summary>
        /// <returns>The new connection.</returns>
        DbConnection GetConnection();

        /// <summary>
        /// Start a new unit of work.
        /// </summary>
        IUnitOfWork CreateUnitOfWork();
        #endregion
    }
}
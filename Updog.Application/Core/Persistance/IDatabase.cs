using System;
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

        void RegisterRepo<TResolve, TRepo>() where TResolve : class, IRepo where TRepo : class, IRepo;

        TRepo GetRepo<TRepo>(DbConnection? connection = null) where TRepo : class, IRepo;
        #endregion
    }
}
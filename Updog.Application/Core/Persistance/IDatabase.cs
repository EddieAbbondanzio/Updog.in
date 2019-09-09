using System;
using System.Data;
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
        IDbConnection GetConnection();

        /// <summary>
        /// Register a repo for use with the database.
        /// </summary>
        /// <typeparam name="TResolve">The interface type it resolves as.</typeparam>
        /// <typeparam name="TRepo">The implementation type.</typeparam>
        void RegisterRepo<TResolve, TRepo>() where TResolve : class, IRepo where TRepo : class, IRepo;

        /// <summary>
        /// Get a CRUD repo for manaing an entity.
        /// </summary>
        /// <typeparam name="TRepo">The type of repo to get.</typeparam>
        /// <param name="connection">The active database connection</param>
        TRepo GetRepo<TRepo>(IDbConnection connection) where TRepo : class, IRepo;
        #endregion
    }
}
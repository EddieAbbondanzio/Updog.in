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
        /// Get a new context with the database.
        /// </summary>
        /// <returns>The new active database context.</returns>
        DatabaseContext GetContext();

        /// <summary>
        /// Register a repo for use with the database.
        /// </summary>
        /// <typeparam name="TResolve">The interface type it resolves as.</typeparam>
        /// <typeparam name="TRepo">The implementation type.</typeparam>
        void RegisterRepo<TResolve, TRepo>() where TResolve : class, IRepo where TRepo : DatabaseRepo;
        #endregion
    }
}
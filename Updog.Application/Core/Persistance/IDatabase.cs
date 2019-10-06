using System;
using System.Data;
using System.Data.Common;
using Updog.Domain;

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
        #endregion
    }
}
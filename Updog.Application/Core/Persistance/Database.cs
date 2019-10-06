using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Updog.Application;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Base class for databases to implement.
    /// </summary>
    public abstract class Database : IDatabase {
        #region Fields
        protected IServiceProvider serviceProvider;
        #endregion

        #region Constructor(s)
        public Database(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a new context with the database.
        /// </summary>
        /// <returns></returns>
        public DatabaseContext GetContext() {
            var connection = GetConnection();
            connection.Open();

            return new DatabaseContext(connection, serviceProvider);
        }
        #endregion

        #region Privates
        protected abstract DbConnection GetConnection();
        #endregion
    }
}
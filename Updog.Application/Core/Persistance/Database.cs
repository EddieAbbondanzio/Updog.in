using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Base class for databases to implement.
    /// </summary>
    public abstract class Database : IDatabase {
        #region Properties
        protected Dictionary<Type, Type> RepoMap;
        #endregion

        #region Constructor(s)
        public Database(IDatabaseConfig config) {
            RepoMap = new Dictionary<Type, Type>();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a new context with the database.
        /// </summary>
        /// <returns></returns>
        public abstract DatabaseContext GetContext();

        /// <summary>
        /// Register a repo with the database.
        /// </summary>
        /// <typeparam name="TResolve">The type it resolves as.</typeparam>
        /// <typeparam name="TRepo">The implementation type.</typeparam>
        public void RegisterRepo<TResolve, TRepo>() where TResolve : class, IRepo where TRepo : DatabaseRepo => RepoMap.Add(typeof(TResolve), typeof(TRepo));
        #endregion
    }
}
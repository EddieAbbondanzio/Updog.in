using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Updog.Application;

namespace Updog.Persistance {
    /// <summary>
    /// Base class for databases to implement.
    /// </summary>
    public abstract class Database : IDatabase {
        #region Fields
        private Dictionary<Type, Type> _repoMap;
        #endregion

        #region Constructor(s)
        public Database() {
            _repoMap = new Dictionary<Type, Type>();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a new active connection with the database.
        /// </summary>
        /// <returns>The new connection.</returns>
        public abstract IDbConnection GetConnection();

        /// <summary>
        /// Register a repo with the database.
        /// </summary>
        /// <typeparam name="TResolve">The type it resolves as.</typeparam>
        /// <typeparam name="TRepo">The implementation type.</typeparam>
        public void RegisterRepo<TResolve, TRepo>() where TResolve : class, IRepo where TRepo : class, IRepo => _repoMap.Add(typeof(TResolve), typeof(TRepo));


        /// <summary>
        /// Resolve a repo.
        /// </summary>
        /// <typeparam name="TRepo">The repo type to resolve.</typeparam>
        public TRepo GetRepo<TRepo>(IDbConnection connection) where TRepo : class, IRepo {
            Type resolveType = typeof(TRepo);

            if (!_repoMap.ContainsKey(resolveType)) {
                throw new NotFoundException($"No repo found for type {typeof(TRepo).Name}. Did you correctly register it?");
            }

            Type repoType = _repoMap[resolveType];

            /*
             * This is a poor design decision. It assumes the IRepo is always a DatabaseRepo that needs
             * a parameter of a db connection. Address this down the road? Or never...
             */
            return (TRepo)Activator.CreateInstance(repoType, connection) as TRepo;
        }
        #endregion
    }
}
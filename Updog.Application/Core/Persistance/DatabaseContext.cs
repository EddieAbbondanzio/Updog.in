using System;
using System.Collections.Generic;
using System.Data.Common;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Context for interacting with the database.
    /// </summary>
    public sealed class DatabaseContext : IDisposable {
        #region Properties
        /// <summary>
        /// The active database connection.
        /// </summary>
        /// <value></value>
        public DbConnection Connection { get; }
        #endregion

        #region Fields
        private Dictionary<Type, Type> repoMap;
        #endregion

        #region Constructor(s)
        public DatabaseContext(DbConnection connection, Dictionary<Type, Type> repoMap) {
            Connection = connection;
            this.repoMap = repoMap;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Resolve a repo.
        /// </summary>
        /// <typeparam name="TRepo">The repo type to resolve.</typeparam>
        public TRepo GetRepo<TRepo>() where TRepo : class, IRepo {
            Type resolveType = typeof(TRepo);

            if (!repoMap.ContainsKey(resolveType)) {
                throw new NotFoundException($"No repo found for type {typeof(TRepo).Name}. Did you correctly register it?");
            }

            Type repoType = repoMap[resolveType];

            // It's safe to assume the repo type will be a DatabaseRepo<T> since we can only register these.
            return (TRepo)Activator.CreateInstance(repoType, this) as TRepo;
        }

        public void Dispose() => Connection.Dispose();
        #endregion

    }
}
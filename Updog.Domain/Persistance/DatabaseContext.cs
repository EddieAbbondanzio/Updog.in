using System;
using System.Collections.Generic;
using System.Data.Common;
using Updog.Domain;

namespace Updog.Domain {
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
        private IServiceProvider serviceProvider;
        #endregion

        #region Constructor(s)
        public DatabaseContext(DbConnection connection, IServiceProvider serviceProvider) {
            Connection = connection;
            this.serviceProvider = serviceProvider;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Resolve a repo.
        /// </summary>
        /// <typeparam name="TRepo">The repo type to resolve.</typeparam>
        public TRepo GetRepo<TRepo>() where TRepo : class, IRepo => (TRepo)serviceProvider.GetService(typeof(TRepo));

        /// <summary>
        /// Resolve a read only reader.
        /// </summary>
        /// <typeparam name="TReader">The reader type to resolve.</typeparam>
        public TReader GetReader<TReader>() where TReader : class, IReader => (TReader)serviceProvider.GetService(typeof(TReader));

        public void Dispose() => Connection.Dispose();
        #endregion

    }
}
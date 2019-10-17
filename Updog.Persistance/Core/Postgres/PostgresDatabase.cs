using System;
using System.Data;
using System.Data.Common;
using Npgsql;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// A database for data persistance that runs PostgreSQL.
    /// </summary>
    public sealed class PostgresDatabase : Database {
        #region Fields
        /// <summary>
        /// The connection string for initiating new connections.
        /// </summary>
        private string connection;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new database.
        /// </summary>
        /// <param name="connection">The connection config.</param>
        public PostgresDatabase(IDatabaseConfig config, IServiceProvider serviceProivder) : base(serviceProivder) {
            connection = config.GetConnectionString();
        }
        #endregion

        #region Publics
        protected override DbConnection GetConnection() => new NpgsqlConnection(this.connection);
        #endregion
    }
}
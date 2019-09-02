using System;
using System.Data.Common;
using Npgsql;

namespace Updog.Persistance {
    /// <summary>
    /// A database for data persistance that runs PostgreSQL.
    /// </summary>
    public sealed class PostgresDatabase : IDatabase {
        #region Properties
        /// <summary>
        /// The connection string for initiating new connections.
        /// </summary>
        public string Connection { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new database.
        /// </summary>
        /// <param name="connection">The connection config.</param>
        public PostgresDatabase(IDatabaseConfig config) {
            NpgsqlConnectionStringBuilder connBuilder = new NpgsqlConnectionStringBuilder() {
                Host = config.Host,
                Port = config.Port,
                Username = config.User,
                Password = config.Password,
                Database = config.Database
            };

            Connection = connBuilder.ToString();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a new connection with the database.
        /// </summary>
        /// <returns>A new pooled connection.</returns>
        public DbConnection GetConnection() {
            return new NpgsqlConnection(Connection);
        }
        #endregion
    }
}

using System.Data;
using System.Data.Common;
using Npgsql;
using Updog.Application;

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
        public PostgresDatabase(IDatabaseConfig config) : base(config) {
            NpgsqlConnectionStringBuilder connBuilder = new NpgsqlConnectionStringBuilder() {
                Host = config.Host,
                Port = config.Port,
                Username = config.User,
                Password = config.Password,
                Database = config.Database
            };

            connection = connBuilder.ToString();
        }
        #endregion

        #region Publics
        public override DatabaseContext GetContext() {
            var connection = new NpgsqlConnection(this.connection);
            connection.Open();

            return new DatabaseContext(connection, RepoMap);
        }
        #endregion
    }
}
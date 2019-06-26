using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Blurtle.Persistance {
    /// <summary>
    /// A database for data persistance that runs MySQL.
    /// </summary>
    public sealed class MySqlDatabase : IDatabase {
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
        /// <param name="connection">The connection string.</param>
        public MySqlDatabase(string connection) {
            Connection = connection;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a new connection with the database.
        /// </summary>
        /// <returns>A new pooled connection.</returns>
        public DbConnection GetConnection() {
            return new MySqlConnection(Connection);
        }
        #endregion
    }
}

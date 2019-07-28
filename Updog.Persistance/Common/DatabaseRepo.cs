using System.Data.Common;
using System.Transactions;
using Blurtle.Domain;

namespace Blurtle.Persistance {
    /// <summary>
    /// Base class for repos to inherit if they work off the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public abstract class DatabaseRepo<TEntity> where TEntity : IEntity {
        #region Fields
        private IDatabase database;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new database based repo.
        /// </summary>
        /// <param name="database">The database to work with.</param>
        public DatabaseRepo(IDatabase database) {
            this.database = database;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Start a scoped transaction with the database.
        /// </summary>
        /// <returns>The newly opened transaction.</returns>
        public TransactionScope BeginTransaction() => new TransactionScope();
        #endregion

        #region Privates
        /// <summary>
        /// Get a pooled connection to the database.
        /// </summary>
        /// <returns>The new connection.</returns>
        protected DbConnection GetConnection() => database.GetConnection();
        #endregion
    }
}
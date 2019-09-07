using System;
using System.Transactions;
using Updog.Application;

namespace Updog.Persistance {
    /// <summary>
    /// A unit of work for managing a set of commands against the database.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork, IDisposable {
        #region Fields
        private TransactionScope _transactionScope;
        #endregion

        #region Constructor(s)
        public UnitOfWork() {
            TransactionOptions opts = new TransactionOptions() {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            _transactionScope = new TransactionScope(TransactionScopeOption.Required, opts);
        }
        #endregion

        #region Publics
        /// <summary>
        /// Save the changes made.
        /// </summary>
        public void Save() => _transactionScope.Complete();

        /// <summary>
        /// Undo the work done.
        /// </summary>
        public void Undo() => _transactionScope.Dispose();

        /// <summary>
        /// Support for using() statement.
        /// </summary>
        void IDisposable.Dispose() => _transactionScope.Dispose();
        #endregion
    }
}
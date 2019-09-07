using System;

namespace Updog.Application {
    /// <summary>
    /// Unit of work (transaction) to wrap actions into.
    /// </summary>
    public interface IUnitOfWork : IDisposable {
        /// <summary>
        /// Save the changes made.
        /// </summary>
        void Save();

        /// <summary>
        /// Undo the changes made.
        /// </summary>
        void Undo();
    }
}
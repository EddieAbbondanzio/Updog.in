namespace Updog.Application {
    /// <summary>
    /// Unit of work (transaction) to wrap actions into.
    /// </summary>
    public interface IUnitOfWork {
        /// <summary>
        /// Save the changes made.
        /// </summary>
        void Commit();

        /// <summary>
        /// Undo the changes made.
        /// </summary>
        void Undo();
    }
}
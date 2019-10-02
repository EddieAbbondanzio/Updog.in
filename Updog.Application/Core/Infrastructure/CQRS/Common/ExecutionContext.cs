namespace Updog.Application {
    /// <summary>
    /// Execution context of a command, or query.
    /// </summary>
    public sealed class ExecutionContext<TAction> {
        #region Properties
        /// <summary>
        /// The action being executed.
        /// </summary>
        /// <value></value>
        public TAction Input { get; }

        /// <summary>
        /// Active connection with the database.
        /// </summary>
        /// <value></value>
        public DatabaseContext Database { get; }

        /// <summary>
        /// Output port to send data back to.
        /// </summary>
        public IOutputPort Output { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new ExecutionContext
        /// </summary>
        /// <param name="input">The action input</param>
        /// <param name="database">The active database context.</param>
        /// <param name="output">The output port to return data to.</param>
        public ExecutionContext(TAction input, DatabaseContext database, IOutputPort output) {
            Input = input;
            Database = database;
            Output = output;
        }
        #endregion
    }
}
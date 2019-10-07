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
        /// Output port to send data back to.
        /// </summary>
        public IOutputPort Output { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new ExecutionContext
        /// </summary>
        /// <param name="input">The action input</param>
        /// <param name="output">The output port to return data to.</param>
        public ExecutionContext(TAction input, IOutputPort output) {
            Input = input;
            Output = output;
        }
        #endregion
    }
}
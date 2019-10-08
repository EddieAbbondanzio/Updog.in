namespace Updog.Application {
    public class CommandResult {
        #region Properties
        public bool Success { get; }
        #endregion

        #region Constructor(s)
        public CommandResult(bool success) {
            Success = success;
        }
        #endregion  
    }
}
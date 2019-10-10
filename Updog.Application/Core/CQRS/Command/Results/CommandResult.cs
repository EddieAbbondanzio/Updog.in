namespace Updog.Application {
    public class CommandResult {
        #region Properties
        public bool IsSuccess { get; }
        public int InsertId { get; }

        public string? Error { get; }
        #endregion

        #region Constructor(s)
        private CommandResult(bool success, int insertId = 0, string? error = null) {
            IsSuccess = success;
            InsertId = insertId;
            Error = error;
        }
        #endregion  

        #region Statics
        public static CommandResult Success(int insertId = 0) => new CommandResult(true, insertId);

        public static CommandResult Failure(string? error = null) => new CommandResult(false, 0, error);
        #endregion
    }
}
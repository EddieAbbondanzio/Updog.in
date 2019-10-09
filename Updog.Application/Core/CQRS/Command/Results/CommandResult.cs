namespace Updog.Application {
    public class CommandResult {
        #region Properties
        public bool Success { get; }
        public int InsertId { get; }
        #endregion

        #region Constructor(s)
        public CommandResult(bool success, int insertId = 0) {
            Success = success;
            InsertId = insertId;
        }
        #endregion  
    }
}
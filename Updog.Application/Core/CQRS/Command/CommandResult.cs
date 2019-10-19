namespace Updog.Application {
    public class CommandResult {
        #region Properties
        public bool IsSuccess { get; }
        public int InsertId { get; }
        #endregion

        #region Constructor(s)
        public CommandResult(bool success, int insertId = 0) {
            IsSuccess = success;
            InsertId = insertId;
        }
        #endregion  

        #region Statics
        public static CommandResult Success() => new CommandResult(true);

        public static CommandResult Insert(int id) => new CommandResult(true, id);
        #endregion
    }
}
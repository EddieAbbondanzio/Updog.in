namespace Updog.Domain {
    public class Error {
        #region Properties
        public string Message { get; }
        #endregion

        #region Constructor(s)
        public Error(string message) {
            Message = message;
        }
        #endregion
    }
}
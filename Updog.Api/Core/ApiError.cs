namespace Updog.Api {
    /// <summary>
    /// Basic errors that need to be returned back to the caller of the API.
    /// </summary>
    public class ApiError {
        #region Properties
        /// <summary>
        /// The type of error it is.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The human readable error message.
        /// </summary>
        public string Message { get; set; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new API error.
        /// </summary>
        /// <param name="type">The type of error it is.</param>
        /// <param name="message">The error message.</param>
        public ApiError(string type, string message) {
            Type = type;
            Message = message;
        }
        #endregion
    }
}
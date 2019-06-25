namespace Blurtle.Api {
    /// <summary>
    /// Error that can be passed back to the caller of the API.
    /// </summary>
    public sealed class ApiError {
        #region Properties
        /// <summary>
        /// The error code.
        /// </summary>
        /// <value></value>
        public int Code { get; }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Error { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new api error.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error"></param>
        public ApiError(int code, string error) {
            Code = code;
            Error = error;
        }
    }
    #endregion
}
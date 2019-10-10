namespace Updog.Application {
    /// <summary>
    /// Property and error for a resource that failed validation.
    /// </summary>
    public sealed class ValidationFailure {
        #region Properties
        /// <summary>
        /// Name of the property on the resource that was validated.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Human readable text of why the error is occuring.
        /// </summary>
        public string ErrorMessage { get; }
        #endregion

        #region Constructor(s)
        public ValidationFailure(string propertyName, string errorMessage) {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
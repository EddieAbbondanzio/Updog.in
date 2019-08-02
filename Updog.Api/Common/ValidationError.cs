using System;

namespace Updog.Api {
    /// <summary>
    /// Validation error that caused a action to fail.
    /// </summary>
    public sealed class ValidationError : ApiError {
        #region Properties
        /// <summary>
        /// The field that failed validation.
        /// </summary>
        /// <value></value>
        public ValidationFailure[] Failures { get; set; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new validation error. 
        /// </summary>
        /// <param name="fails">The validation failures.</param>
        public ValidationError(ValidationFailure[] fails) : base("validation", "A validation error occured.") {
            Failures = fails;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

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

        /// <summary>
        /// Create a new validation error from a validation exception.
        /// </summary>
        /// <param name="exception">The validation exception that was thrown.</param>
        public ValidationError(ValidationException exception) : base("validation", "A validation error occured.") {
            Failures = exception.Errors.Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage)).ToArray();
        }
        #endregion
    }
}
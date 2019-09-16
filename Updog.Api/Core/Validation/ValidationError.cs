using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Updog.Api.Validation {
    /// <summary>
    /// Validation error that caused a action to fail.
    /// </summary>
    public sealed class ValidationFailure {
        #region Properties
        /// <summary>
        /// The field that failed validation.
        /// </summary>
        /// <value></value>
        public IEnumerable<ValidationError> Errors { get; set; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new validation error. 
        /// </summary>
        /// <param name="errors">The validation failures.</param>
        public ValidationFailure(IEnumerable<ValidationError> errors) {
            Errors = errors;
        }
        #endregion
    }
}
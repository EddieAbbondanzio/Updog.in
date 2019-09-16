using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Updog.Api.Validation {
    /// <summary>
    /// Factory to build validation failure summaries to send back to the user.
    /// </summary>
    public static class ValidationFailureFactory {
        /// <summary>
        /// Generate a validation failure from a validation result.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>The generated validation failure.</returns>
        public static ValidationFailure FromResult(ValidationResult result) {
            return new ValidationFailure(result.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)));
        }
    }
}

using System.Collections.Generic;

namespace Updog.Application {
    public sealed class ValidationResult {
        #region Properties
        /// <summary>
        /// If the validation completed successfully.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Validation failures that occured if any.
        /// </summary>
        public IEnumerable<ValidationFailure> Failures { get; }
        #endregion

        #region Constructor(s)
        public ValidationResult() {
            IsValid = true;
            Failures = new List<ValidationFailure>();
        }

        public ValidationResult(IEnumerable<ValidationFailure> failures) {
            IsValid = false;
            Failures = failures;
        }
        #endregion

        #region Statics
        /// <summary>
        /// Create a new successful validation result with no errors.
        /// </summary>
        public static ValidationResult Success() => new ValidationResult();

        /// <summary>
        /// Create a new validation result that failed.
        /// </summary>
        /// <param name="failures">The errors of why it failed.</param>
        public static ValidationResult Fail(IEnumerable<ValidationFailure> failures) => new ValidationResult(failures);
        #endregion
    }
}
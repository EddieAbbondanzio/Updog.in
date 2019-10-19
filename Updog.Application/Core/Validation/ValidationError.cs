using Updog.Domain;

namespace Updog.Application {
    public sealed class ValidationError : Error {
        #region Properties
        public ValidationResult Result { get; }
        #endregion

        #region Constructor(s)
        public ValidationError(ValidationResult result, string? error = null) : base(error ?? "A validation error occured.") {
            Result = result;
        }
        #endregion
    }
}
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via it's unique ID.
    /// </summary>
    internal sealed class FindValidator : IPagableValidator<FindParams> {
        #region Constructor(s)
        public FindValidator() {
        }
        #endregion
    }

}
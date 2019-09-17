using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via the user that created it..
    /// </summary>
    internal sealed class FindByUserValidator : FluentValidatorAdapter<FindByValueParams<string>> {
        #region Constructor(s)
        public FindByUserValidator() {
            RuleFor(c => c.Value).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Username is required.");
        }
        #endregion
    }

}
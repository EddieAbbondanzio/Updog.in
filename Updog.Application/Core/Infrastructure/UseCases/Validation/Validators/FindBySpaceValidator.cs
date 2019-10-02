using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via the space it was submitted to.
    /// </summary>
    internal sealed class FindBySpaceValidator : IPagableValidator<FindByValueParams<string>> {
        #region Constructor(s)
        public FindBySpaceValidator() {
            RuleFor(c => c.Value).NotNull().WithMessage("Space is required.");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Space is required.");
        }
        #endregion
    }

}
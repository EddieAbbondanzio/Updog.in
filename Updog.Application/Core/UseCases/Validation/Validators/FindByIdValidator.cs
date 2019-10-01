using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via it's unique ID.
    /// </summary>
    internal sealed class FindByIdValidator : IPagableValidator<FindByValueParams<int>> {
        #region Constructor(s)
        public FindByIdValidator() {
            RuleFor(c => c.Value).GreaterThan(0).WithMessage("Id of resource to find is required.");
        }
        #endregion
    }

}
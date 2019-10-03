using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via the user that created it..
    /// </summary>
    internal sealed class FindUserByUsernameQueryValidator : FluentValidatorAdapter<FindUserByUsernameQuery> {
        #region Constructor(s)
        public FindUserByUsernameQueryValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");
        }
        #endregion
    }

}